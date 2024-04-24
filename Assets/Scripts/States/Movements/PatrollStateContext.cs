using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.Contexts
{
    public class PatrollStateContext: IPatrollStateContext
    {
        public float Distance { get; set; }
        public float Speed { get; set; }
        public float RadioDetection { get; set; }
        public float RadioAttack { get; set; }
        public Vector2 Direction { get; set; }
        
        public PatrollStateContext()
        {
            
        }

        public PatrollStateContext(float  distance, float speed, float radioDetection, float radioAttack, Vector2 direction):this()
        {
            Distance = distance;
            Speed = speed;
            RadioDetection = radioDetection;
            RadioAttack = radioAttack;
            Direction = direction;  
        }
    }
}
