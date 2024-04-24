using Assets.Scripts.Entities;
using Assets.Scripts.States.Contexts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttack : MonoBehaviour
{
    protected PlayerEntity player;

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEntity>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.CompareTag("Enemies"))
        {
            Debug.Log("Make Damage");
            var entity = collision.gameObject.GetComponent<EnemyEntity>();
            if (entity != null)
            {
                Debug.Log("Make Damage to Enemy");
                entity.TryApplyDamage(new DamageStateContext( -1, player.transform.position, 10f));
                //enemy.ReciveDamage(player.transform.position, player.ForceDamage);
            }
            
        }
    }
}
