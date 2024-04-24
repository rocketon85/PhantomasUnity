using UnityEngine;

namespace Assets.Scripts.States.Contexts
{
    public interface IAttackStateContext: IStateContext
    {
        public abstract float Duration { get; set; }
        public abstract Vector2 Direction { get; set; }
    }
}
