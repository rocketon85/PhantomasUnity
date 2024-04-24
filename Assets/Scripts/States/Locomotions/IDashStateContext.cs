using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.Contexts
{
    public interface IDashStateContext: IStateContext
    {
        public abstract float Speed { get; set; }
        public abstract float Duration { get; set; }
    }
}
