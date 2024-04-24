using Assets.Scripts.States;
using Assets.Scripts.States.Contexts;
using UnityEngine;

namespace Assets.Scripts.Behaviors.Movements
{
    public interface IWalkable:IMovable
    {

        #region Public Properties
        public abstract IWalkStateContext GetContext();

        #endregion

        #region Public Methods
        public abstract bool IsValidWalk();
        #endregion
    }
}
