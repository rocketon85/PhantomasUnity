using Assets.Scripts.Behaviors.Movements;
using Assets.Scripts.States.Contexts;
using Assets.Scripts.States.Locomotions;
using Cinemachine;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.States
{
    public class DashState : LocomotionState
    {
        private new DashStateContext Context => (DashStateContext)base.Context;


        protected CinemachineVirtualCamera cinemachineVirtualCamera;

        public DashState(IDashable behavior, IDashStateContext ctx) : base(behavior, ctx) { 
            cinemachineVirtualCamera = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
        }
        
        public override void OnEnter(IStateController sc)
        {
            isRunning = true;
            base.OnEnter(sc);

            Anim.SetBool("IsRolling", true);

            Vector3 positionPlayer = Camera.main.WorldToViewportPoint(Behavior.GetGameObject.transform.position);
            Camera.main.GetComponent<RippleEffect>().Emit(positionPlayer);
            Behavior.StartCoroutine(ShakeCamera());

            Rigidbody.velocity = Vector2.zero;
            Rigidbody.velocity += DirectionRaw.normalized * Context.Speed;
            Behavior.StartCoroutine(StartingRoll());
        }
        public override void OnUpdate()
        {
            base.OnUpdate();

            
        }
        public override void OnExit()
        {
            base.OnExit();
            Rigidbody.gravityScale = 10;
            Anim.SetBool("IsRolling", false);
        }

        private IEnumerator ShakeCamera(float time = 0.3f)
        {
            CinemachineBasicMultiChannelPerlin cmPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cmPerlin.m_AmplitudeGain = 5;

            yield return new WaitForSeconds(time);
            cmPerlin.m_AmplitudeGain = 0;
        }

        private IEnumerator StartingRoll()
        {
            Behavior.StartCoroutine(RollFloor());

            Rigidbody.gravityScale = 0;
            yield return new WaitForSeconds(Context.Duration);
            OnExit();
        }

        private IEnumerator RollFloor()
        {
            yield return new WaitForSeconds(0.15f);
           
        }
    }
}
