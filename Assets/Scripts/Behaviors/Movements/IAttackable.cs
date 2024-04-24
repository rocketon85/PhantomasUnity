using Assets.Scripts.States;
using Assets.Scripts.States.Contexts;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Behaviors.Actions
{
    public interface IAttackable:IBehavior
    {
        #region Public Properties
        public abstract float RadioAttack { get; }
        public abstract IWeapon Weapon { get; }

        public abstract IAttackStateContext GetContext();
        #endregion

        #region Public Methods
        public abstract bool IsValidAttack();
        #endregion
    }
}
