using Assets.Scripts.Entities;
using Assets.Scripts.Weapons;
using Assets.Scripts.Weapons.Range;
using UnityEngine;

public class EnemyArcherSkeleton : EnemyRangeEntity
{
    protected new BowWeapon weapon;

    protected override void Awake()
    {  
        base.Awake();   
        RadioAttack = 10f;
        VectorHead = new Vector2(-0.2f, 1f);
        durationAttack = 1.2f;
        gameObject.name = "skeleton";

        weapon = new BowWeapon();
        weapon.Owner = this;
        weapon.EnemyTarget = player;
        weapon.MaxArrow = 5;
        weapon.TimeRecharge = 1f;
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
    }

    void Start()
    {
       
    }

    protected override void Update()
    {
        base.Update();
    }

}
