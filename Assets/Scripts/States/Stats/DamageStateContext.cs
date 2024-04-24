using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.Contexts
{
    public class DamageStateContext: IDamageStateContext
    {
        public float Damage { get; set; }
        public Vector3 Direction { get; set; }
        public float Impulse { get; set; }
        
        public DamageStateContext()
        {
            
        }

        public DamageStateContext(float damage, Vector3 direction, float impulse)
        {
            Damage = damage;
            Direction = direction;  
            Impulse = impulse;
        }
    }
}
