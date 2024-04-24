using Assets.Scripts.Behaviors.Stats;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.States.Stats
{
    public class DieState :BaseState
    {
        private SpriteRenderer spriteRenderer;

        public DieState(IDieable behavior) : base(behavior) {
            spriteRenderer = Behavior.GetGameObject.GetComponent<SpriteRenderer>();
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
            Anim.SetBool("IsDying", true);

            Object.Destroy(Behavior.GetGameObject, 0.2f);
            OnExit();
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
        }
        public override void OnExit()
        {
            base.OnExit();
            Anim.SetBool("IsDying", false);

            ((IDieable)Behavior).Die();
        }
    }
}
