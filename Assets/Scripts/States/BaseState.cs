using Assets.Scripts.Behaviors;
using Assets.Scripts.Entities;
using Assets.Scripts.States.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.States
{
    public class BaseState : IState
    {
        private IStateContext context;

        protected bool isRunning = false;
        protected IStateController StateMachine;
        protected IBehavior Behavior;
        protected Animator Anim;

        public IStateContext Context => context;

        public BaseState(IBehavior behavior)
        {
            Behavior = behavior;
            Anim = Behavior.GetGameObject.GetComponent<Animator>();
        }

        public BaseState(IBehavior behavior, IStateContext ctx):this(behavior) 
        { 
            context = ctx;
        }

        public virtual void OnEnter(IStateController sc)
        {
            StateMachine = sc;
        }

        public virtual void OnExit()
        {
            isRunning = false;
        }

        public virtual void OnUpdate()
        {

        }

        public bool IsRunning()
        {
            return isRunning;
        }
    }
}
