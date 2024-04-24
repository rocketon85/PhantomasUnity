using Assets.Scripts.Behaviors.Actions;
using Assets.Scripts.Behaviors.Stats;
using Assets.Scripts.Entities;

namespace Assets.Scripts.Weapons
{
    public interface IWeapon
    {
        public IAttackable Owner { get; set; }
        public IDamageable EnemyTarget { get; set; }
        public float Damage { get; }

        public void DoDamage(IDamageable entity);
    }
}
