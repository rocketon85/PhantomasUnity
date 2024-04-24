using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Behaviors.Movements
{
    public interface IMovable:IBehavior
    {
        #region Public Properties
        public abstract float Speed { get; }

        #endregion
    }
}
