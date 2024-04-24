using Assets.Scripts.Behaviors.Actions;
using Assets.Scripts.Behaviors.Stats;
using Assets.Scripts.Entities;
using Assets.Scripts.States.Contexts;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class BaseWeapon : MonoBehaviour, IWeapon
    {
        protected IAttackable owner;
        protected IDamageable enemyTarget;
        protected Rigidbody2D rigidbody;
        protected BoxCollider2D boxCollider;

        [Header("Character")]
        public LayerMask layerFloor;

        [Header("Stats")]
        public float Damage = 0f;

        public IAttackable Owner { get => owner; set => owner = value; }
        public IDamageable EnemyTarget { get => enemyTarget; set => enemyTarget = value; }
        float IWeapon.Damage { get => Damage; }


        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision != null && enemyTarget != null && collision.gameObject == enemyTarget.GetGameObject)
            {
                DoDamage(enemyTarget);
                
            }
        }

        public void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            boxCollider = GetComponent<BoxCollider2D>();
        }

        protected void Update()
        {
            
        }

        public virtual void DoDamage(IDamageable entity) {
            throw new System.NotImplementedException();
        }
    }
}
