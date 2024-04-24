using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.States
{
    public interface IStateController
    {
        public abstract void Update();
        public abstract IState GetCurrentState();
        public abstract void ChangeState(IState newState);
        public abstract bool IsRunningState();
    }
}
