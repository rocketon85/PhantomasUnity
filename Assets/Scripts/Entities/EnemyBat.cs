using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using System.Linq;
using Assets.Scripts.Entities;
using Assets.Scripts.Behaviors.Actions;

public class EnemyBat : EnemyMeleeEntity
{
    protected override void Awake()
    {  
        base.Awake();   
        RadioAttack = 0f;
        VectorHead = new Vector2(0.2f, 0.6f);
        gameObject.name = "bat";
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
