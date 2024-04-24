using Assets.Scripts.Behaviors.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapons.Range
{
    public class BowWeapon:BaseWeapon
    {
        [Header("Stats")]
        public int MaxArrow = 3;
        public float TimeRecharge = 4f;

        public GameObject arrow;


        private void OnCollisionEnter2D(Collision2D collision)
        {
            
        }

        public new void Awake()
        {
            
        }

        public new void DoDamage(IDamageable entity)
        {
            GameObject arrowGo = Instantiate(arrow, transform.position, Quaternion.identity);
            arrowGo.transform.GetComponent<ArrowWeapon>().EnemyTarget = enemyTarget;
            arrowGo.transform.GetComponent<ArrowWeapon>().Owner = this.owner;
        }
    }
}
