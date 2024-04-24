using Assets.Scripts.Behaviors.Actions;
using Assets.Scripts.Behaviors.Movements;
using Assets.Scripts.Behaviors.Stats;
using Assets.Scripts.States;
using Assets.Scripts.States.Contexts;
using Assets.Scripts.States.Stats;
using Assets.Scripts.Weapons.Range;
using System;
using System.Linq;
using UnityEngine;


namespace Assets.Scripts.Entities
{
    public class EnemyRangeEntity : EnemyEntity, IRangeAttackable
    {

        float IRangeAttackable.RadioAttack => RadioAttack;

        protected override void Awake()
        {
            base.Awake();
            RadioAttack = 10f;
        }

        protected override void Update()
        {
            base.Update();


            IState newState = GetNextState();

            if (newState != null)
            {
                ChangeState(newState);
            }
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, RadioAttack);
        }

        public override IState GetNextState()
        {
            if (IsValidRangeAttack())
                return new AttackState(this, ((IRangeAttackable)this).GetContext());
            else
                return base.GetNextState();
        }

        IAttackStateContext IRangeAttackable.GetContext()
        {
            return new AttackStateContext(durationAttack, directionAttack, RadioAttack);
        }

        public bool IsValidRangeAttack()
        {
            if (!Enumerable.Range(0, Convert.ToInt32(RadioAttack)).Contains(Convert.ToInt32(DistanceToPlayer)))
                return false;
            if (IsRunningState() && GetCurrentState() is AttackState)
                return false;
            return true;
        }
    }
}
