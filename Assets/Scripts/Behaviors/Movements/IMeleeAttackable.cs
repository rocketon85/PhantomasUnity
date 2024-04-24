using Assets.Scripts.States;
using Assets.Scripts.States.Contexts;
using UnityEngine;

namespace Assets.Scripts.Behaviors.Actions
{
    public interface IMeleeAttackable:IAttackable
    {
        #region Public Properties
        public abstract IAttackStateContext GetContext();
        #endregion

        #region Public Methods
        public abstract bool IsValidMeleeAttack();
        #endregion
    }
}
