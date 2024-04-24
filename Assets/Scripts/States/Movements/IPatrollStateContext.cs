using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.Contexts
{
    public interface IPatrollStateContext: IStateContext
    {
        public abstract float Distance { get; set; }
        public abstract float Speed { get; set; }
        public abstract float RadioDetection { get; set; }
        public abstract float RadioAttack { get; set; }
        public abstract Vector2 Direction { get; set; }

    }
}
