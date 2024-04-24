using Assets.Scripts.States;
using Assets.Scripts.States.Contexts;
using UnityEngine;

namespace Assets.Scripts.Behaviors.Actions
{
    public interface IRangeAttackable:IAttackable
    {
        #region Public Properties
        public abstract float RadioAttack { get; }
        public abstract IAttackStateContext GetContext();
        #endregion

        #region Public Methods
        public abstract bool IsValidRangeAttack();
        #endregion
    }
}
