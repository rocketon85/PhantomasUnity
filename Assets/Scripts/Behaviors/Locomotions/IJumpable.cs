using Assets.Scripts.States;
using Assets.Scripts.States.Contexts;
using UnityEngine;

namespace Assets.Scripts.Behaviors.Movements
{
    public interface IJumpable: IMovable
    {

        #region Public Properties
        public abstract IJumpStateContext GetContext();
        #endregion

        #region Public Methods
        public abstract bool IsValidJump();
        #endregion
    }
}
