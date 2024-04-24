using Assets.Scripts.Behaviors.Stats;
using Assets.Scripts.States.Contexts;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.States.Stats
{
    public class DamageState :BaseState
    {
        private new IDamageStateContext Context => (IDamageStateContext)base.Context;

        private SpriteRenderer spriteRenderer;
        private Rigidbody2D rigidbody;


        private ApplyDamage funCallback;
        public delegate float ApplyDamage(float damage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="behavior"></param>
        /// <param name="damageReceived">Damage Received</param>
        public DamageState(IDamageable behavior, IDamageStateContext context, ApplyDamage callback) : base(behavior, context) {
            spriteRenderer = Behavior.GetGameObject.GetComponent<SpriteRenderer>();
            rigidbody = Behavior.GetGameObject.GetComponent<Rigidbody2D>();            

            funCallback = callback;
        }

        protected virtual IEnumerator DamageEffect()
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.color = Color.white;
        }

        public override void OnEnter(IStateController sc)
        {
            base.OnEnter(sc);
            Anim.SetBool("IsHurt", true);

            var health = funCallback(Context.Damage);
            if (health > 0)
            {
               Behavior.StartCoroutine(DamageEffect());
            }
            else
            {
                StateMachine.ChangeState(new DieState((IDieable)Behavior));
            }
        }
        public override void OnUpdate()
        {
            base.OnUpdate();

            if (Context.Direction != Vector3.zero)
            {
                rigidbody.AddForce((Behavior.GetGameObject.transform.position - Context.Direction).normalized * Context.Impulse, ForceMode2D.Impulse);
                Context.Direction = Vector3.zero;
            }
        }
        public override void OnExit()
        {
            base.OnExit();
            Anim.SetBool("IsHurt", false);
        }
    }
}
