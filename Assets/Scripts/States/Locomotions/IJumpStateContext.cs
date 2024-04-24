﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.Contexts
{
    public interface IJumpStateContext: IStateContext
    {
        public abstract float Force { get; set; }
        public abstract Vector2 Direction { get; set; }
    }
}
