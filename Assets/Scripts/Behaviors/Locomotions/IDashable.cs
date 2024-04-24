using Assets.Scripts.States;
using Assets.Scripts.States.Contexts;
using UnityEngine;


namespace Assets.Scripts.Behaviors.Movements
{
    public interface IDashable:IMovable
    {

        #region Public Properties
        public abstract IDashStateContext GetContext();
        #endregion

        #region Public Methods
        public abstract bool IsValidDash();
        #endregion
    }
}
