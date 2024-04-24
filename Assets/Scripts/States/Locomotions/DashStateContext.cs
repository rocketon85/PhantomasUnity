using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.Contexts
{
    public class DashStateContext: IDashStateContext
    {
        public float Speed { get; set; }
        public float Duration { get; set; }

        public DashStateContext()
        {
            
        }

        public DashStateContext(float speed, float duration)
        {
            Speed = speed;
            Duration = duration;
        }
    }
}
