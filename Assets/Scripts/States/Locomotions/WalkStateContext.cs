using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.Contexts
{
    public class WalkStateContext: IWalkStateContext
    {
        public float Speed { get; set; }
        
        public WalkStateContext()
        {
            
        }

        public WalkStateContext(float speed)
        {
            Speed=speed;
        }
    }
}
