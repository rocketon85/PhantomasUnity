using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.Contexts
{
    public class JumpStateContext: IJumpStateContext
    {
        public Vector2 Direction { get; set; }
        public float Force { get; set; }

        public JumpStateContext()
        {
            
        }

        public JumpStateContext(float force, Vector2 direction)
        {
            Force = force;
            Direction = direction;
        }
    }
}
