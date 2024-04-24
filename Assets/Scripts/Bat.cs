using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using System.Linq;

public class Bat : Enemy
{
    private const float radioAttack = 0f;
    private const float radioDetection = 15f;


    protected override void Awake()
    {
        RadioDetection = radioDetection;
        RadioAttack = radioAttack;

        base.Awake();
        
        VectorHead = new Vector2(0.2f, 0.6f);
        
        gameObject.name = "bat";
    }
    //private void Awake()
    //{
    //    rigidbody = GetComponent<Rigidbody2D>();
    //    spriteRenderer = GetComponent<SpriteRenderer>();
    //    animator = GetComponent<Animator>();
    //    cinemachineVirtualCamera = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();

    //    player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    //}

    protected override void OnDrawGizmosSelected()
    {
        RadioDetection = radioDetection;
        RadioAttack = radioAttack;

        base.OnDrawGizmosSelected();
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("bat collision");
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        Debug.Log("bat collision with player");
    //        if (HitOnHead)
    //        {
    //            Debug.Log("bat collision on head");
    //            player.GetComponent<Rigidbody2D>().velocity = Vector2.up * player.ForceJump;
    //            //StartCoroutine(ShakeCamera(3)); //check on player
    //            Die();
    //        }
    //        else
    //        {
    //            Debug.Log("bat collision another place");
    //            player.ReciveDamage((transform.position - player.transform.position).normalized, ForceDamage);
    //        }
    //    }
    //}

    //private void Die()
    //{
    //    Destroy(gameObject, 0.2f);
    //}

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    //void Update()
    //{
    //    directionMove = player.transform.position - transform.position;
    //    Move();
    //}

    //private void Move()
    //{
    //    float distance = Vector2.Distance(transform.position, player.transform.position);
    //    if (RadioDetection <= distance) { 
    //        rigidbody.velocity = directionMove.normalized * SpeedMove;
    //        SetView(directionMove.normalized.x);
    //    }
    //    else
    //    {
    //        rigidbody.velocity = Vector2.zero;

    //    }

    //    HitOnHead = Physics2D.OverlapBox((Vector2)transform.position + VectorHead, new Vector2(1, 0.05f) * 0.7f, 0, LayerPlayer);

    //}

    //private void SetView(float x)
    //{
    //    if (x < 0 && transform.localScale.x > 0)
    //        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    //    else if(x>0 && transform.localScale.x < 0)
    //        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    //}

    ////private void fix

    //public override void ReciveDamage(Vector3 direction, float damage)
    //{
    //    Debug.Log("Bat Recibe Damage");
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
