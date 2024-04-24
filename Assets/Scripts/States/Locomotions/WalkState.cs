using Assets.Scripts.Behaviors.Movements;
using Assets.Scripts.States.Contexts;
using UnityEngine;

namespace Assets.Scripts.States.Locomotions
{
    public class WalkState : LocomotionState
    {
        private new IWalkStateContext Context => (IWalkStateContext)base.Context;

        public WalkState(IWalkable behavior, IWalkStateContext ctx) : base(behavior, ctx) {  }
        
        public override void OnEnter(IStateController sc)
        {
            base.OnEnter(sc);
            Anim.SetBool("IsWalking", true);
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            if (Direction != Vector2.zero)
            {
                Anim.SetBool("IsWalking", true);

                Rigidbody.velocity = new Vector2(Direction.x * Context.Speed, Rigidbody.velocity.y);
            }
        }
        public override void OnExit()
        {
            base.OnExit();
            Anim.SetBool("IsWalking", false);
        }
    }
}
