using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : Player {

    //config
    [SerializeField] private float movingSpeedOnLadder = 1f;
    [SerializeField] private float defaultMovingSpeed = 1f;
    [SerializeField] private float climbingLadderSpeed = 1f;
    [SerializeField] private float jumpingStrenght = 1f;
    [SerializeField] private float pullingRange = 1f;

    [SerializeField] private LayerMask crateMask;

    //state
    public float currentMovingSpeed;
    private GameObject crateToPull;

    void Start ()
    {
        currentMovingSpeed = defaultMovingSpeed;
        Player.IsActive = true;
        Physics2D.queriesStartInColliders = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale * pullingRange);
    }

    protected override void Update () {
        base.Update();
        if (isActive)
        {
            MovingHorizontal();
            Jumping();
            PullingCrate();
            ClimbingLadder();
            FlipSide();
            SetMovingSpeed();
        }
    }

    private void MovingHorizontal()
    {
        animator.SetBool("Running", Mathf.Abs(xAxisInput) > Mathf.Epsilon);  // running animation
        myRigidbody.velocity = new Vector2(xAxisInput * currentMovingSpeed, myRigidbody.velocity.y);
    }

    private void Jumping()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump") && isTouchingGround && !isPullingCrate)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpingStrenght);
        }
    }

    private void ClimbingLadder()
    {
        animator.SetBool("OnLadder", isTouchingLadder && !isTouchingGround);
        animator.SetBool("Climbing", isTouchingLadder && (yAxisInput != 0 || xAxisInput != 0));

        if (isTouchingLadder && !isTouchingGround)
        {
            myRigidbody.gravityScale = 0;
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, yAxisInput * climbingLadderSpeed);
        }
        else
        {
            myRigidbody.gravityScale = 1;
        }
    }

    private void PullingCrate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale, pullingRange, crateMask);
        if(hit.collider != null && CrossPlatformInputManager.GetButton("Fire1"))
        {
            PullingAnimation(true);
            isPullingCrate = true;
            crateToPull = hit.collider.gameObject;
            crateToPull.GetComponent<FixedJoint2D>().enabled = true;
            crateToPull.GetComponent<FixedJoint2D>().connectedBody = myRigidbody;
        }
        else if(CrossPlatformInputManager.GetButtonUp("Fire1"))
        {
            PullingAnimation(false);
            isPullingCrate = false;
            if (crateToPull)crateToPull.GetComponent<FixedJoint2D>().enabled = false;
        }
    }

    private void PullingAnimation(bool isPulling)
    {
        animator.SetBool("PushingIdle", isPulling);
        if (isPulling)
        {
            animator.SetBool("Pushing", xAxisInput * transform.localScale.x > 0);
            animator.SetBool("Pulling", xAxisInput * transform.localScale.x < 0);
        }
        else
        {
            animator.SetBool("Pushing", false);
            animator.SetBool("Pulling", false);
        }

    }

    private void FlipSide()
    {
        if (xAxisInput > 0 && !isPullingCrate)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
        else if(xAxisInput < 0 && !isPullingCrate)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
    }

    private void SetMovingSpeed()
    {
        if(isTouchingLadder && !isTouchingGround)
        {
            currentMovingSpeed = movingSpeedOnLadder;
        }
        else
        {
            currentMovingSpeed = defaultMovingSpeed;
        }
    }

}
