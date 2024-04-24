using Assets.Scripts.Behaviors.Movements;
using UnityEngine;
using System.Collections;
using Assets.Scripts.States.Locomotions;
using Assets.Scripts.States.Contexts;

namespace Assets.Scripts.States
{
    public class JumpState : LocomotionState
    {
        private new IJumpStateContext Context => (IJumpStateContext)base.Context;

        public JumpState(IJumpable behavior, IJumpStateContext ctx) : base(behavior, ctx) { }

        private IEnumerator StartingJump()
        {
            Rigidbody.gravityScale = 3;
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, 0);
            Rigidbody.velocity += Context.Direction * Context.Force;
            
            yield return new WaitForSeconds(0.5f);
            OnExit();
        }

        public override void OnEnter(IStateController sc)
        {
            base.OnEnter(sc);
            isRunning = true;
            Anim.SetBool("IsJumping", true);

            Behavior.StartCoroutine(StartingJump());
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public override void OnExit()
        {
            base.OnExit();
            // code that runs when we exit the state
            Anim.SetBool("IsJumping", false);
        }
    }
}
