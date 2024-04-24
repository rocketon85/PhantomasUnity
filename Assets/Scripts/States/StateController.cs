using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.States
{
    public class StateController: IStateController
    {
        IState currentState = null;
        public void Update()
        {
            currentState?.OnUpdate();
        }
        public IState GetCurrentState()
        {
            return currentState;
        }
        public void ChangeState(IState newState)
        {
            if (newState == null) return;

            currentState?.OnExit();
            currentState = newState;
            currentState?.OnEnter(this);
        }

        public bool IsRunningState()
        {
            return currentState != null ? currentState.IsRunning() : false;
        }
    }
}
