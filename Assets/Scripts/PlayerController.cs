using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : Character
{
    private float durationRoll = 0.5f;
    private float durationAttack = 0.2f;
    private float consecutiveJumps = 0f;
    private float consecutiveRolls = 0f;

    [Header("Booleans")]
    public bool CanGrip = false;
    public bool CanRoll = true;
    public bool IsRolling = false;
    public bool IsClimbing = false;
    public bool IsJumpingFromWall = false;
    public bool IsOnRigthWall = false;
    public bool IsOnLeftWall = false;

    [Header("Statics")]
    public float SpeedRoll = 20f;
    public float SpeedClimb = 1f;
    public float MaxConsecutiveJumps = 2f;
    public float MaxConsecutiveRolls = 2f;

    protected override void Awake()
    {
        base.Awake();
        Health = 100f;
        Impulse = 25f;
    }


    #region Coroutines
    private IEnumerator StartingRoll()
    {
        StartCoroutine(RollFloor());

        rigidbody.gravityScale = 0;
        IsRolling = true;

        yield return new WaitForSeconds(durationRoll);

        rigidbody.gravityScale = 3;
        IsRolling = false;
    }

    private IEnumerator RollFloor()
    {
        yield return new WaitForSeconds(0.15f);
        if (CanMove)
            CanRoll = false;
    }


    #endregion

    #region Protecteds
    protected override void CheckGrip()
    {
        base.CheckGrip();
        if(IsOnFloor)
        {
            consecutiveJumps = 0;
            consecutiveRolls = 0;
        }
        //else
        //{
        //    if(consecutiveJumps < MaxConsecutiveJumps)
        //    {
        //        CanJump = true;
        //    }
        //}
        IsOnLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + VectorWallLeft, RadiusCollision, LayerFloor);
        IsOnRigthWall = Physics2D.OverlapCircle((Vector2)transform.position + VectorWallRight, RadiusCollision, LayerFloor);
        CanGrip = IsOnLeftWall || IsOnRigthWall;
    }
    #endregion



    private void Roll(float x, float y)
    {
        consecutiveRolls++;
        if(consecutiveRolls <= MaxConsecutiveRolls)
        {
            Debug.Log($"cant rolling {consecutiveRolls}");

            animator.SetBool("IsRolling", true);
            //Camera.main.WorldToViewportPoint -> get player position respect of camera.
            Vector3 positionPlayer = Camera.main.WorldToViewportPoint(transform.position);
            Camera.main.GetComponent<RippleEffect>().Emit(positionPlayer);
            StartCoroutine(ShakeCamera());

            rigidbody.velocity = Vector2.zero;
            rigidbody.velocity += new Vector2(x, y).normalized * SpeedRoll;
            StartCoroutine(StartingRoll());
        }
        else
        {
            CanRoll = false;
        }

    }

    private void TouchFloor()
    {
        IsRolling = false;
        IsClimbing = false;
        IsJumpingFromWall = false;

        CanRoll = true;
        IsOnFloor = true;

        animator.SetBool("IsJumping", false);
        animator.SetBool("IsFallingDown", false);
        animator.SetBool("IsClimbing", false);
    }

    private void Climb()
    {
        if (IsClimbing)
        {
            animator.SetBool("IsClimbing", true);
            if (rigidbody.velocity == Vector2.zero)
                animator.SetFloat("SpeedClimb", 0);
            else
                animator.SetFloat("SpeedClimb", 1);
        }
        else
        {
            animator.SetBool("IsClimbing", false);
            animator.SetFloat("SpeedClimb", 0);
        }
    }

    private void ClimbWall()
    {
        if (CanMove)
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, -SpeedClimb);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        CheckGrip();
        Move();
    }

    public void EndRoll()
    {
        animator.SetBool("IsRolling", false);
        CanRoll = true;
    }

    public void Lock()
    {
        IsLocked = true;
    }

    protected override void Move()
    {
        base.Move();

        if (CanMove && !IsRolling && !IsAttacking)
            Walk();
        //else
        //    if (IsLocked) EndAttack();

        IsClimbing = CanGrip && Input.GetKey(KeyCode.LeftShift);
        Climb();

        if (IsClimbing && !IsRolling)
        {
            rigidbody.gravityScale = 0;
            if (x > 0.2f && x < -0.2f)
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
            float speedVelocity = y > 0 ? 0.5f : 1;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, y * (SpeedMove * speedVelocity));

            if (IsOnLeftWall && transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else if (IsOnRigthWall && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            rigidbody.gravityScale = 3;
        }

        if (CanGrip && !IsOnFloor)
        {
            if (x != 0 && !IsClimbing)
                ClimbWall();
        }

        if (rigidbody.velocity.y != 0)
            UpgradeJump();

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.GetKeyDown(KeyCode.X) && CanRoll && (xRaw != 0 || yRaw != 0))
            Roll(xRaw, yRaw);

        if (Input.GetKeyDown(KeyCode.Z) && !IsAttacking)
            Attack(AttackDirection(directionMove, directionRaw));

        if (CanMove && !IsOnFloor)
            TouchFloor();

        if (!CanMove && IsOnFloor)
            IsOnFloor = false;

        float velocity;
        if (rigidbody.velocity.y >= 0)
            velocity = 1;
        else
            velocity = -1;

        //if (IsOnFloor)
        //    velocity = 0;

        if (!CanJump)
        {
            animator.SetFloat("SpeedVertical", velocity);
        }
        else
        {
            if (velocity == -1)
                EndJump();
        }
    }

    public void UpgradeJump()
    {
        if (rigidbody.velocity.y < 0)
        {
            rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (2.5f - 1) * Time.deltaTime;
        }
        else if (rigidbody.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (2.0f - 1) * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemies")
        {
            
        }
    }

    public override void ReciveDamage(Vector3 direction, float damage)
    {
        Debug.Log("Player damaged");
        base.ReciveDamage(direction, damage);
    }

    private void JumpFromWall()
    {
        Debug.Log("jumping from wall");
        StartCoroutine(DisableMovements(0));
        StartCoroutine(DisableMovements(0.1f));

        Vector2 directionWall = IsOnRigthWall ? Vector2.left : Vector2.right;

        if (directionWall.x < 0 && transform.localScale.x > 0)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        else if (directionWall.x > 0 && transform.localScale.x < 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        animator.SetBool("IsJumping", true);
        animator.SetBool("IsClimbing", false);
        Jump((Vector2.up + directionWall));
        IsJumpingFromWall = true;
    }

    
    public void Jump()
    {
        consecutiveJumps++;

        if (consecutiveJumps <= MaxConsecutiveJumps)
            CanJump = true;
        else
            CanJump = false;

        if (CanJump)
        {
            Debug.Log($"cant jump {consecutiveJumps}");

            Jump(Vector2.up);
            //IsOnFloor = false;
            //IsClimbing = false;

            //animator.SetBool("IsFallingDown", false);
            //animator.SetBool("IsRolling", false);
            //animator.SetBool("IsWalking", false);
            //animator.SetBool("IsClimbing", false);

            //animator.SetBool("IsJumping", true);
            
            //rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
            //rigidbody.velocity += Vector2.up * ForceJump;
        }
        else if (IsClimbing) //if player is grip on the wall.
        {
            animator.SetBool("IsClimbing", false);
            JumpFromWall();
        }

    }

    private void Jump(Vector2 directionJump)
    {
        IsOnFloor = false;
        IsClimbing = false;

        animator.SetBool("IsJumping", true);



        rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
        rigidbody.velocity += directionJump * ForceJump;
    }

    public void EndJump()
    {
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsFallingDown", true);
    }

    public void Walk()
    {
        if (direction != Vector2.zero)
        {
            if (IsJumpingFromWall)
            {
                rigidbody.velocity = Vector2.Lerp(rigidbody.velocity,
                    (new Vector2(direction.x * SpeedMove, rigidbody.velocity.y)), Time.deltaTime / 2);
            }
            else
            {
                rigidbody.velocity = new Vector2(direction.x * SpeedMove, rigidbody.velocity.y);

                if (!CanJump)
                {
                    animator.SetBool("IsJumping", true);
                }
                else
                {
                    animator.SetBool("IsJumping", false);
                    animator.SetBool("IsWalking", true);
                }

                if (direction.x < 0 && transform.localScale.x > 0)
                {
                    directionMove = AttackDirection(Vector2.left, direction);
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
                else if (direction.x > 0 && transform.localScale.x < 0)
                {
                    directionMove = AttackDirection(Vector2.right, direction);
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
            }

        }
        else
        {
            if (direction.y > 0 && direction.x == 0)
                directionMove = AttackDirection(direction, Vector2.up);
            if (direction.y < 0 && direction.x == 0)
                directionMove = AttackDirection(direction, Vector2.down);

            animator.SetBool("IsWalking", false);
        }


    }
}
