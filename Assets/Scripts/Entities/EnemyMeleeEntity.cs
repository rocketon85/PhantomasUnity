using Assets.Scripts.Behaviors.Actions;
using Assets.Scripts.Behaviors.Movements;
using Assets.Scripts.Behaviors.Stats;
using Assets.Scripts.States;
using Assets.Scripts.States.Contexts;
using Assets.Scripts.States.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Assets.Scripts.Entities
{
    public class EnemyMeleeEntity : EnemyEntity, IMeleeAttackable
    {
        float IAttackable.RadioAttack => 0f;

        protected override void Awake()
        {
            base.Awake();
            RadioAttack = 0f;
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
        }

        public override IState GetNextState()
        {
            if (IsValidMeleeAttack())
                return new AttackState(this, ((IMeleeAttackable)this).GetContext());
            else
                return base.GetNextState();
        }

        IAttackStateContext IMeleeAttackable.GetContext()
        {
            return new AttackStateContext(durationAttack, directionAttack);
        }

        public bool IsValidMeleeAttack()
        {
            if (!Enumerable.Range(0, 10).Contains(Convert.ToInt32(DistanceToPlayer)))
                return false;
            if (IsRunningState() && GetCurrentState() is AttackState)
                return false;
            return true;
        }
    }
}
