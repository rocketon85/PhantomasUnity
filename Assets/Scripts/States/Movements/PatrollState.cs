using Assets.Scripts.Behaviors.Actions;
using UnityEngine;
using System.Collections;
using Assets.Scripts.States.Contexts;
using static Cinemachine.AxisState;
using static Cinemachine.CinemachineImpulseManager.ImpulseEvent;
using Unity.VisualScripting;
using Assets.Scripts.States.Locomotions;

namespace Assets.Scripts.States
{
    public class PatrollState : BaseState
    {
        protected Rigidbody2D Rigidbody;

        private new IPatrollStateContext Context => (IPatrollStateContext)base.Context;

        public PatrollState(IPatrollable behavior, IPatrollStateContext ctx) : base(behavior, ctx)
        {
            Rigidbody = Behavior.GetGameObject.GetComponent<Rigidbody2D>();
        }


        public override void OnEnter(IStateController sc)
        {
            base.OnEnter(sc);
            isRunning = true;

            Anim.SetBool("IsWalking", true);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (Context.Distance <= Context.RadioDetection)
            {
                // Create composite for add behivor to Enemy (mele enemy, throw enemy or both)
                if (Context.RadioAttack > 0)
                {
                    if (Context.Distance > Context.RadioAttack)
                    {
                        Anim.SetBool("IsWalking", true);
                        Rigidbody.velocity = new Vector2(Context.Direction.x, 0).normalized * Context.Speed;
                    }
                    else
                    {
                        Anim.SetBool("IsWalking", false);
                        Rigidbody.velocity = Vector2.zero;
                    }
                }
                else
                {
                    Anim.SetBool("IsWalking", true);
                    Rigidbody.velocity = Context.Direction.normalized * Context.Speed;
                }
            }
            else
            {
                Anim.SetBool("IsWalking", false);
                Rigidbody.velocity = Vector2.zero;
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            // code that runs when we exit the state
            Anim.SetBool("IsWalking", false);
        }
    }
}
