using Assets.Scripts.Behaviors;
using Assets.Scripts.States;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class BaseEntity : MonoBehaviour, IBehavior
    {
        #region StateMachine
        private IStateController stateController;
        IStateController IBehavior.SC { get => stateController; }
        #endregion

        #region Vars
        protected Rigidbody2D rigidbody;

        protected Vector2 direction, directionMove, directionRaw;
        protected Vector2 vectorFloor;

        protected float x, y, xRaw, yRaw;
        #endregion

        #region Setters
        [Header("Statics")]
        public bool IsOnFloor;

        #endregion

        #region Public Properties
        public GameObject GetGameObject { get => gameObject;  }
        #endregion

        //public Vector2 VectorFloor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        #region IEntity
        void IBehavior.StartCoroutine(IEnumerator routine)
        {
            base.StartCoroutine(routine);
        }
        #endregion

        #region Private Methods
        protected Vector2 GetDirectionMove(Vector2 dirMov, Vector2 dirRaw)
        {
            if (rigidbody.velocity.x == 0 && dirRaw.y != 0)
                return new Vector2(0, dirRaw.y);
            return new Vector2(dirMov.x, dirRaw.y);
        }
        #endregion

        #region Protected Methods
        protected void ChangeState(IState newState)
        {
            stateController?.ChangeState(newState);
        }

        protected void UpdateState()
        {
            stateController?.Update();
        }
        protected IState GetCurrentState()
        {
            return stateController?.GetCurrentState();
        }

        protected bool IsRunningState()
        {
            return stateController != null ? stateController.IsRunningState() : false;
        }
        protected bool CheckGrip(Vector2 vector, float radius, LayerMask layer)
        {
            return Physics2D.OverlapCircle(vector, radius, layer);
        }
        #endregion

        #region MonoBehaviour
        protected virtual void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();

            stateController = new StateController();
            vectorFloor = new Vector2(0f, -0.8f);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        protected virtual void Update()
        {
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");
            xRaw = Input.GetAxisRaw("Horizontal");
            yRaw = Input.GetAxisRaw("Vertical");

            direction = new Vector2(x, y);
            directionRaw = new Vector2(xRaw, yRaw);

            stateController?.Update();
            if (direction != Vector2.zero)
            {
                if (direction.x < 0 && transform.localScale.x > 0)
                {
                    directionMove = GetDirectionMove(Vector2.left, direction);
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
                else if (direction.x > 0 && transform.localScale.x < 0)
                {
                    directionMove = GetDirectionMove(Vector2.right, direction);
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
            }
            else
            {
                if (direction.y > 0 && direction.x == 0)
                    directionMove = GetDirectionMove(direction, Vector2.up);
                if (direction.y < 0 && direction.x == 0)
                    directionMove = GetDirectionMove(direction, Vector2.down);
            }
            //CheckGrip();
            //Move();
        }

        private void FixedUpdate()
        {
            stateController?.Update();
        }
        public virtual IState GetNextState()
        {
            if (GetCurrentState() is IdleState || IsRunningState())
                return null;
            else
                return new IdleState(this);
        }
        #endregion


    }
}
