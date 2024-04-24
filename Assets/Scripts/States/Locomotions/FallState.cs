
using Assets.Scripts.Behaviors.Movements;
using Assets.Scripts.States.Locomotions;

namespace Assets.Scripts.States
{
    public class FallState : LocomotionState
    {
        const float gravitityScale = 3;

        public FallState(IJumpable behavior) : base(behavior) {  }

        public override void OnEnter(IStateController sc)
        {
            base.OnEnter(sc);
            Anim.SetBool("IsFallingDown", true);

            Rigidbody.gravityScale = gravitityScale;
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            Rigidbody.gravityScale = gravitityScale;
        }
        public override void OnExit()
        {
            base.OnExit();
            // code that runs when we exit the state
            Anim.SetBool("IsFallingDown", false);
        }
    }
}
