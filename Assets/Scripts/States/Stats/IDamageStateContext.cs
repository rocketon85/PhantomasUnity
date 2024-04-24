using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.Contexts
{
    public interface IDamageStateContext: IStateContext
    {
        public abstract float Damage { get; set; }
        public abstract Vector3 Direction { get; set; }
        public abstract float Impulse { get; set; }
    }
}
