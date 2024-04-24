using Assets.Scripts.Behaviors.Stats;
using Assets.Scripts.States.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapons.Range
{
    public class ArrowWeapon:BaseWeapon
    {
        public void Awake()
        {
            base.Awake();

            Damage = 1f;
        }

        protected void Update()
        {
            base.Update();

        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D (collision);
            if (collision.IsTouchingLayers(layerFloor))
            {
                rigidbody.bodyType = RigidbodyType2D.Static;
                boxCollider.enabled = false;
                this.enabled = false;
            }
        }

        public override void DoDamage(IDamageable entity)
        {
            enemyTarget.TryApplyDamage(new DamageStateContext(Damage, -(enemyTarget.GetGameObject.transform.position - owner.GetGameObject.transform.position).normalized, 5f));
            Destroy(this.gameObject);
        }
    }
}
