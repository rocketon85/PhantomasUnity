using Assets.Scripts.Behaviors.Actions;
using Assets.Scripts.Behaviors.Movements;
using Assets.Scripts.Behaviors.Stats;
using Assets.Scripts.States;
using Assets.Scripts.States.Contexts;
using Assets.Scripts.States.Locomotions;
using Assets.Scripts.States.Stats;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class PlayerEntity : BaseEntity, IJumpable, IWalkable, IDashable, IAttackable, IDamageable
    {

        private float powerDamage = 1f;
        private float durationRoll = 0.5f;
        private float durationAttack = 0.3f;
        private byte consecutiveJump = 0;
        private byte consecutiveDash = 0;

        protected Vector2 directionJump;
        protected Vector2 directionRoll;
        protected Vector2 directionAttack;

        #region Setters
        [Header("Statics")]
        public float Health = 3f;
        public float SpeedMove = 7f;
        public float ForceJump = 15f;
        public float ForceDamage = 1f;
        public float RadiusCollision = 0.1f;
        public float Impulse = 50f;
        public float SpeedRoll = 20f;
        public float SpeedClimb = 1f;
        public byte MaxConsecutiveJumps = 2;
        public byte MaxConsecutiveDashs = 2;

        [Header("Collisions")]
        public LayerMask LayerFloor;

        [Header("Vectors")]
        public Vector2 VectorFloor = new Vector2(0f, -0.8f);

        float IAttackable.RadioAttack => throw new System.NotImplementedException();

        float IDamageable.Health => throw new System.NotImplementedException();

        Vector2 IDamageable.VectorHead => throw new System.NotImplementedException();

        float IMovable.Speed => SpeedMove;

        IWeapon IAttackable.Weapon => throw new System.NotImplementedException();

        IDamageStateContext IDamageable.GetContext() => throw new System.NotImplementedException();
        #endregion

        protected override void Awake()
        {
            base.Awake();

            SpeedMove = 10f;
            ForceJump = 12f;
            SpeedRoll = 20f;
            durationRoll = 0.5f;
        }

        protected override void Update()
        {
            IState newState = null;

            base.Update();
            IsOnFloor = Physics2D.OverlapCircle((Vector2)transform.position + VectorFloor, RadiusCollision, LayerFloor);
            if (IsOnFloor)
            {
                consecutiveDash = 0;
                consecutiveJump = 0;
            }

            directionJump = Vector2.up;
            directionRoll = new Vector2(xRaw, yRaw);
            directionAttack = GetDirectionMove(directionMove, directionRaw);

            newState = GetNextState();

            if (newState != null)
            {
                ChangeState(newState);
            }
        }

        public override IState GetNextState()
        {
            if (IsValidJump())
                return new JumpState(this, ((IJumpable)this).GetContext());
            else if (IsValidDash())
                return new DashState(this, ((IDashable)this).GetContext());
            else if (IsValidAttack())
                return new AttackState(this, ((IAttackable)this).GetContext() );
            else if (IsValidWalk() && !IsRunningState())
                return new WalkState(this, ((IWalkable)this).GetContext());
            else if (!IsRunningState() && IsOnFloor)
                return new IdleState(this);
            else
                return base.GetNextState();
        }

        public bool IsValidJump()
        {
            if (!Input.GetKeyDown(KeyCode.Space))
                return false;

            consecutiveJump++;
            if (consecutiveJump >= MaxConsecutiveJumps)
                return false;

            return true;
        }

        public bool IsValidDash()
        {
            if (!Input.GetKeyDown(KeyCode.X))
                return false;

            consecutiveDash++;
            if (consecutiveDash >= MaxConsecutiveDashs)
                return false;
            if (IsRunningState() && GetCurrentState() is DashState)
                return false;

            return true;
        }

        public bool IsValidWalk()
        {
            if (direction != Vector2.zero)
                return true;
            return false;
        }

        public bool IsValidAttack()
        {
            if (!Input.GetKeyDown(KeyCode.Z))
                return false;
            if (IsRunningState() && GetCurrentState() is AttackState)
                return false;
            return true;
        }

        public bool IsValidHurt()
        {
            if (!Input.GetKeyDown(KeyCode.Z))
                return false;

            return true;
        }


        public void TryApplyDamage(IDamageStateContext ctx)
        {
            ChangeState(new DamageState(this, ctx, ApplyDamage));
        }

        float ApplyDamage(float damage)
        {
            Health -= damage;
            return Health;
        }

        IDashStateContext IDashable.GetContext()
        {
            return new DashStateContext(SpeedRoll, durationRoll);
        }

        IJumpStateContext IJumpable.GetContext()
        {
            return new JumpStateContext(ForceJump, directionJump);
        }

        IAttackStateContext IAttackable.GetContext()
        {
            return new AttackStateContext(durationAttack, directionAttack);
        }

        IWalkStateContext IWalkable.GetContext()
        {
            return new WalkStateContext(SpeedMove);
        }

        bool IAttackable.IsValidAttack()
        {
            throw new System.NotImplementedException();
        }
    }
}
