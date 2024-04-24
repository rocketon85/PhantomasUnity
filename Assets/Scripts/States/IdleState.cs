using Assets.Scripts.Behaviors;

namespace Assets.Scripts.States
{
    public class IdleState : BaseState
    {
        public IdleState(IBehavior gObject) : base(gObject) { }
        
        public override void OnEnter(IStateController sc)
        {
            base.OnEnter(sc);
            //Anim.SetBool("IsFallingDown", false);
        }
        public override void OnUpdate()
        {
            // Here we add logic to detect if the conditions exist to
            // transition to another state
        }
        public override void OnExit()
        {
            // code that runs when we exit the state
        }
    }
}
