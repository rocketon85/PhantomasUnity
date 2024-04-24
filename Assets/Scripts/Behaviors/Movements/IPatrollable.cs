using Assets.Scripts.States;
using Assets.Scripts.States.Contexts;
using UnityEngine;

namespace Assets.Scripts.Behaviors.Actions
{
    public interface IPatrollable:IBehavior
    {
        #region Public Properties
        public float DistanceToPlayer { get; }
        public float RadioDetection { get; }
        public abstract IPatrollStateContext GetContext();
        #endregion


        #region Public Methods
        public abstract void SetView(float x);
        public abstract bool IsValidPatroll();
        #endregion
    }
}
