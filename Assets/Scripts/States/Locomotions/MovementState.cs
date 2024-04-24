using Assets.Scripts.Behaviors.Movements;
using Assets.Scripts.States.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Cinemachine.AxisState;

namespace Assets.Scripts.States.Locomotions
{
    public class LocomotionState : BaseState
    {
        protected Rigidbody2D Rigidbody;
        protected Vector2 Direction;
        protected Vector2 DirectionRaw;

        public LocomotionState(IMovable behavior) : base(behavior)
        {
            Rigidbody = Behavior.GetGameObject.GetComponent<Rigidbody2D>();
        }
        public LocomotionState(IMovable behavior, IStateContext ctx) : base(behavior, ctx) {
            Rigidbody = Behavior.GetGameObject.GetComponent<Rigidbody2D>();
        }


        protected void GetDirections()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            float xRaw = Input.GetAxisRaw("Horizontal");
            float yRaw = Input.GetAxisRaw("Vertical");

            Direction = new Vector2(x, y);
            DirectionRaw = new Vector2(xRaw, yRaw);
        }

        public override void OnEnter(IStateController sc)
        {
            base.OnEnter(sc);
            GetDirections();
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            GetDirections();

            if (Direction.x < 0 && Behavior.GetGameObject.transform.localScale.x > 0)
            {
                Behavior.GetGameObject.transform.localScale = new Vector3(-Behavior.GetGameObject.transform.localScale.x, Behavior.GetGameObject.transform.localScale.y, Behavior.GetGameObject.transform.localScale.z);
            }
            else if (Direction.x > 0 && Behavior.GetGameObject.transform.localScale.x < 0)
            {
                Behavior.GetGameObject.transform.localScale = new Vector3(Mathf.Abs(Behavior.GetGameObject.transform.localScale.x), Behavior.GetGameObject.transform.localScale.y, Behavior.GetGameObject.transform.localScale.z);
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
