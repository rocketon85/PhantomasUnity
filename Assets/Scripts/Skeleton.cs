using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    private const float radioAttack = 10f;
    private const float radioDetection = 15f;


    [Header("Statics")]
    public float ForceTrhow = 3f;

    [Header("Objects")]
    public GameObject Arrow;

    protected override void Awake()
    {
        base.Awake();
        RadioDetection = radioDetection;
        RadioAttack = radioAttack;

        VectorHead = new Vector2(0.2f, 0.6f);
    }

    protected override void OnDrawGizmosSelected()
    {
        RadioDetection = radioDetection;
        RadioAttack = radioAttack;
        
        base.OnDrawGizmosSelected();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "skeleton";
    }

    // Update is called once per frame
    protected override void Update()
    {
        //base.Update();
        //Vector2 direction = (player.transform.position - transform.position).normalized * RadioAttack;
        //Debug.DrawRay(transform.position, direction, Color.red);
    }


}
