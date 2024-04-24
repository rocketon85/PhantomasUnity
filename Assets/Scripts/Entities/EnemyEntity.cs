using Assets.Scripts.Behaviors.Actions;
using Assets.Scripts.Behaviors.Movements;
using Assets.Scripts.Behaviors.Stats;
using Assets.Scripts.States;
using Assets.Scripts.States.Contexts;
using Assets.Scripts.States.Stats;
using Assets.Scripts.Weapons;
using System;
using UnityEngine;


namespace Assets.Scripts.Entities
{
    public class EnemyEntity : BaseEntity, IWalkable, IDamageable, IAttackable, IPatrollable, IDieable
    {
        protected PlayerEntity player;
        protected IWeapon weapon;
        protected Vector2 directionToTarget;

        protected float distance;
        protected float durationAttack = 0.1f;
        protected Vector2 directionAttack;

        [Header("Bools")]
        public bool HitOnHead = false;

        [Header("Stats")]
        public float Health = 10f;
        public float RadioDetection = 15f;
        public float RadioAttack = 0f;
        public float SpeedMove = 5f;

        [Header("Vectors")]
        public Vector2 VectorHead;

        float IDamageable.Health { get => Health; }
        float IPatrollable.RadioDetection { get => RadioDetection; }


        Vector2 IDamageable.VectorHead => VectorHead;

        float IMovable.Speed => SpeedMove;

        public float DistanceToPlayer => distance;

        float IAttackable.RadioAttack => throw new NotImplementedException();
        IWeapon IAttackable.Weapon {get => weapon; }

        IDamageStateContext IDamageable.GetContext() => throw new NotImplementedException();

        protected override void Awake()
        {
            base.Awake();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEntity>();

        }

        protected override void Update()
        {
            base.Update();

            directionToTarget = player.transform.position - transform.position;
            distance = Vector2.Distance(transform.position, player.transform.position);
            directionAttack = GetDirectionMove(directionMove, directionRaw);

            ((IPatrollable)this).SetView(directionToTarget.normalized.x);

            IState newState = GetNextState();

            if (newState != null)
            {
                ChangeState(newState);
                if(newState is AttackState && weapon != null)
                {
                    weapon.DoDamage(player);
                }
            }
        }

        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, RadioDetection);
            
            Gizmos.color = Color.green;
            Gizmos.DrawCube((Vector2)transform.position + VectorHead, new Vector2(1, 0.05f) * 1f);
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                var playerCollision = collision.gameObject.GetComponent<PlayerEntity>();

                HitOnHead = (transform.position.y + VectorHead.y < playerCollision.transform.position.y - 0.9f);
                if (HitOnHead)
                {
                    player.GetComponent<Rigidbody2D>().velocity = Vector2.up * playerCollision.ForceJump;
                    //StartCoroutine(ShakeCamera(3)); //check on player
                    ChangeState(new DieState(this));
                }
                else
                {
                    var impulse = 10f;
                    //do enemy go a litle back
                    ChangeState(new AttackState(this, ((IAttackable)this).GetContext()));

                    ((IDamageable)playerCollision).TryApplyDamage(new DamageStateContext(-2, (transform.position - playerCollision.transform.position), impulse));
                    //playerCollision.ReciveDamage((transform.position - player.transform.position).normalized, ForceDamage);
                }
            }
        }

        public override IState GetNextState()
        {
            if (IsValidPatroll())
                return new PatrollState(this, ((IPatrollable)this).GetContext());
            else if (IsValidAttack())
                return new AttackState(this, ((IAttackable)this).GetContext());
            else
                return base.GetNextState();
        }

        public void TryApplyDamage(IDamageStateContext ctx)
        {
            ChangeState(new DamageState(this, ctx, ApplyDamage));
        }

        bool IDamageable.IsValidHurt()
        {
            throw new NotImplementedException();
        }


        float ApplyDamage(float damage)
        {
            Health -= damage;
            return Health;
        }

        public bool IsValidAttack()
        {
            return false;
        }

        IAttackStateContext IAttackable.GetContext()
        {
            return new AttackStateContext(durationAttack, directionAttack);
        }

        void IPatrollable.SetView(float x)
        {
            if (x < 0 && transform.localScale.x > 0)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            else if (x > 0 && transform.localScale.x < 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        public bool IsValidPatroll()
        {
            if(DistanceToPlayer >= RadioDetection)
                return false;
            if(RadioAttack > 0 && DistanceToPlayer <= RadioAttack)
                return false;
            return true;
        }

        IPatrollStateContext IPatrollable.GetContext()
        {
            return new PatrollStateContext(DistanceToPlayer, SpeedMove, RadioDetection, RadioAttack, directionToTarget);
        }

        bool IWalkable.IsValidWalk()
        {
            throw new NotImplementedException();
        }

        IWalkStateContext IWalkable.GetContext()
        {
            return new WalkStateContext(SpeedMove);
        }

        bool IDieable.IsValidDie()
        {
            return true;
        }

        void IDieable.Die()
        {

        }
    }
}
