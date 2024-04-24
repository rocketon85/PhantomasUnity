using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.Contexts
{
    public class AttackStateContext: IAttackStateContext
    {
        public float Radio {  get; set; }
        public float Duration { get; set; }
        public Vector2 Direction { get; set; }
        
        public AttackStateContext()
        {
            
        }

        public AttackStateContext(float duration, Vector2 direction, float radio = 0)
        {
            Duration = duration;
            Direction = direction;  
            Radio = radio;
        }
    }
}
