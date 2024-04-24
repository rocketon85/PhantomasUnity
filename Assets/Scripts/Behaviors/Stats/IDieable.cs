using Assets.Scripts.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Behaviors.Stats
{
    public interface IDieable : IBehavior
    {
        #region Public Properties
        
        #endregion

        #region Public Methods
        public abstract bool IsValidDie();
        public abstract void Die();
        #endregion
    }
}
