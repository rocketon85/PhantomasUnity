using Assets.Scripts.States;
using Cinemachine;
using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{


    protected Rigidbody2D rigidbody;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected CinemachineVirtualCamera cinemachineVirtualCamera;
    protected Vector2 direction, directionRaw, directionMove;
    protected Vector3 directionDamageRecived = Vector3.zero;
    protected float x, y, xRaw, yRaw;
    

    [Header("Booleans")]
    public bool IsLocked = false;
    public bool CanMove = true;
    public bool CanJump = true;
    public bool IsShakingCamera = false;
    public bool IsAttacking = false;
    public bool IsOnFloor = false;

    [Header("Statics")]
    public float Health = 3f;
    public float SpeedMove = 7f;
    public float ForceJump = 15f;
    public float ForceDamage = 1f;
    public float RadiusCollision = 0.1f;
    public float Impulse = 50f;

    [Header("Vectors")]
    public Vector2 VectorFloor = new Vector2(0f, -0.8f);
    public Vector2 VectorWallLeft = new Vector2(-0.5f, 0f);
    public Vector2 VectorWallRight = new Vector2(0.5f, 0f);

    [Header("Collisions")]
    public LayerMask LayerFloor;

    protected virtual void Awake()
    {
      

        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        cinemachineVirtualCamera = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
    }


    #region Coroutines
    protected virtual IEnumerator ShakeCamera(float time = 0.3f)
    {
        IsShakingCamera = true;
        CinemachineBasicMultiChannelPerlin cmPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cmPerlin.m_AmplitudeGain = 5;

        yield return new WaitForSeconds(time);
        cmPerlin.m_AmplitudeGain = 0;
        IsShakingCamera = false;
    }

    protected virtual IEnumerator StartingAttack(Vector2 dir, float time = 11f)
    {
        IsAttacking = true;
        animator.SetFloat("AttackX", dir.x);
        animator.SetFloat("AttackY", dir.y);

        animator.SetBool("IsAttacking", true);

        yield return new WaitForSeconds(time);
        EndAttack();
    }
    #endregion

    #region Protecteds
    protected virtual void CheckGrip()
    {
        IsOnFloor = Physics2D.OverlapCircle((Vector2)transform.position + VectorFloor, RadiusCollision, LayerFloor);
        CanJump = IsOnFloor;
    }

    protected void Attack(Vector2 dir)
    {
        StartCoroutine(StartingAttack(dir));
        //IsAttacking = true;
        //animator.SetFloat("AttackX", dir.x);
        //animator.SetFloat("AttackY", dir.y);

        //animator.SetBool("IsAttacking", true);
    }
    
    protected Vector2 AttackDirection(Vector2 dirMov, Vector2 dirRaw)
    {
        if (rigidbody.velocity.x == 0 && dirRaw.y != 0)
            return new Vector2(0, dirRaw.y);
        return new Vector2(dirMov.x, dirRaw.y);
    }
    protected virtual void Move()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        xRaw = Input.GetAxisRaw("Horizontal");
        yRaw = Input.GetAxisRaw("Vertical");

        direction = new Vector2(x, y);
        directionRaw = new Vector2(xRaw, yRaw);
    }

    protected IEnumerator DisableMovements(float time)
    {
        CanMove = false;
        yield return new WaitForSeconds(time);
        CanMove = true;
    }

    protected virtual void Die()
    {
        Destroy(gameObject, 0.2f);
    }

    protected virtual IEnumerator DamageEffect()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = Color.white;

    }
    #endregion



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //CheckGrip();
        //Move();
    }

    public virtual void EndAttack()
    {
        IsAttacking = false;
        IsLocked = false;
        animator.SetBool("IsAttacking", false);
    }

    
    private void FixedUpdate()
    {
        if (directionDamageRecived != Vector3.zero)
        {
            rigidbody.AddForce((transform.position - directionDamageRecived).normalized * Impulse, ForceMode2D.Impulse);
            directionDamageRecived = Vector3.zero;
        }
    }
    public virtual void ReciveDamage(Vector3 direction, float damage)
    {
        Debug.Log("Character Recibe Damage");
        directionDamageRecived = direction;
        Health -= damage;

        if (Health > 0)
        {
            StartCoroutine(DamageEffect());
        }
        else
        {
            Die();
        }
    }
}
