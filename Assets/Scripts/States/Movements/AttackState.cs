using Assets.Scripts.Behaviors.Actions;
using UnityEngine;
using System.Collections;
using Assets.Scripts.States.Contexts;

namespace Assets.Scripts.States
{
    public class AttackState : BaseState
    {
        private new IAttackStateContext Context => (IAttackStateContext)base.Context;

        public AttackState(IAttackable behavior, IAttackStateContext ctx) : base(behavior, ctx) {  }

        private IEnumerator StartingAttack()
        {
            Anim.SetFloat("AttackX",  Context.Direction.x);
            Anim.SetFloat("AttackY", Context.Direction.y);

            yield return new WaitForSeconds(Context.Duration);
            OnExit();
        }

        public override void OnEnter(IStateController sc)
        {
            base.OnEnter(sc);
            isRunning = true;
            Anim.SetBool("IsAttacking", true);

            Behavior.StartCoroutine(StartingAttack());
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public override void OnExit()
        {
            base.OnExit();
            // code that runs when we exit the state
            Anim.SetBool("IsAttacking", false);
        }
    }
}
