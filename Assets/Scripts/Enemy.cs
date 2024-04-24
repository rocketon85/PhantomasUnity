using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using System.Linq;

public class Enemy : Character
{
    protected PlayerController player;

    [Header("Statics")]
    public float RadioDetection = 10f;
    public float RadioAttack = 10f;

    [Header("Booleans")]
    public bool HitOnHead = false;

    [Header("Vectors")]
    public Vector2 VectorHead = new Vector2(0.1f, 0.7f);

    [Header("Collisions")]
    public LayerMask LayerPlayer;


    protected override void Awake()
    {
        base.Awake();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RadioDetection);
        Gizmos.color = Color.green;
        Gizmos.DrawCube((Vector2)transform.position + VectorHead, new Vector2(1, 0.05f) * 1f);

        if (RadioAttack > 0)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, RadioAttack);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerCollision = collision.gameObject.GetComponent<PlayerController>();

            HitOnHead = (transform.position.y + VectorHead.y < playerCollision.transform.position.y - 0.9f);
            if (HitOnHead)
            {
                player.GetComponent<Rigidbody2D>().velocity = Vector2.up * playerCollision.ForceJump;
                //StartCoroutine(ShakeCamera(3)); //check on player
                Die();
            }
            else
            {
                directionDamageRecived = transform.position;
                Impulse = 50f;
                playerCollision.ReciveDamage((transform.position - player.transform.position).normalized, ForceDamage);
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "enemy";
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //directionMove = player.transform.position - transform.position;
        //Move();
    }

    protected override void Move()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        SetView(directionMove.normalized.x);

        if (distance <= RadioDetection)
        {
            // Create composite for add behivor to Enemy (mele enemy, throw enemy or both)
            if(RadioAttack > 0)
            {
                if(distance > RadioAttack)
                {
                    animator.SetBool("IsWalking", true);
                    rigidbody.velocity = new Vector2(directionMove.x, 0).normalized * SpeedMove;
                }
                else
                {
                    animator.SetBool("IsWalking", false);
                    rigidbody.velocity = Vector2.zero;
                }
            }
            else
            {
                animator.SetBool("IsWalking", true);
                rigidbody.velocity = directionMove.normalized * SpeedMove;
            }
        }
        else
        {
            animator.SetBool("IsWalking", false);
            rigidbody.velocity = Vector2.zero;
        }

        //HitOnHead = Physics2D.OverlapBox((Vector2)transform.position + VectorHead, new Vector2(1, 0.05f) * 0.7f, 0, LayerPlayer);

    }

    private void SetView(float x)
    {
        if (x < 0 && transform.localScale.x > 0)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        else if (x > 0 && transform.localScale.x < 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    //private void fix

    //public override void ReciveDamage(Vector3 direction, float damage)
    //{
    //    Debug.Log("Enemy Recibe Damage");
    //    float impulse = 100f;
    //    Health -= damage;
    //    if (Health > 0)
    //    {

    //    }
    //    else
    //    {
    //        Die();
    //    }


    //    if (true)
    //    {
    //        rigidbody.AddForce((transform.position - direction).normalized * impulse, ForceMode2D.Impulse);
    //    }
    //}
}
