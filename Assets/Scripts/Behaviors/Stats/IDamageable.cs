using Assets.Scripts.States.Contexts;
using UnityEngine;

namespace Assets.Scripts.Behaviors.Stats
{
    public interface IDamageable:IBehavior
    {
        #region Public Properties
        public abstract float Health { get; }
        public abstract Vector2 VectorHead { get; }

        public abstract IDamageStateContext GetContext();
        #endregion


        #region Public Methods
        public abstract bool IsValidHurt();
        public abstract void TryApplyDamage(IDamageStateContext ctx);
        #endregion
    }
}
