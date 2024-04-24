using Assets.Scripts.States;
using Assets.Scripts.States.Contexts;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Behaviors
{
    public interface IBehavior
    {
        #region StateMachine
        protected IStateController SC { get; }
        
        #endregion

        #region Properties
        public GameObject GetGameObject { get; }
        #endregion

        #region Public Methods
        public void StartCoroutine(IEnumerator routine);
        public abstract IState GetNextState();
        #endregion
    }
}
