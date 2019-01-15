using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : Player {

    //config
    [SerializeField] private float runningSpeedOnLadder = 1f;
    [SerializeField] private float runningSpeedWithCrate = 1f;
    [SerializeField] private float defaultRunningSpeed = 1f;
    [SerializeField] private float climbingLadderSpeed = 1f;
    [SerializeField] private float jumpingStrenght = 1f;

    //state
    public float currentRunningSpeed;

    void Start ()
    {
        currentRunningSpeed = defaultRunningSpeed;
        Player.IsActive = true;
    }
	
	protected override void Update () {
        base.Update();
        if (isActive)
        {
            Running();
            Jumping();
            PullingCrate();
            ClimbingLadder();
            SetRunningSpeed();
        }
    }

    private void Running()
    {
        bool isRunning = Mathf.Abs(xAxisInput) > Mathf.Epsilon;
        animator.SetBool("Running", isRunning);  // running animation
        myRigidbody.velocity = new Vector2(xAxisInput * currentRunningSpeed, myRigidbody.velocity.y);
        if (isTouchingGround && !isPullingCrate)
        {
           transform.localScale = new Vector2(Mathf.Sign(xAxisInput), 1f); // Mathf.sign returns 1 or -1 - fliping sprite to runing direction
        }
        else if (isPullingCrate)
        {
            transform.localScale = new Vector2(Mathf.Sign(xAxisInput * -1f), 1f);
        }
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
        if (CrossPlatformInputManager.GetButton("Fire1") && crateToPull && isTouchingGround)
        {
            isPullingCrate = true;
            crateToPull.GetComponent<Rigidbody2D>().velocity = myRigidbody.velocity * 1.17f;
        }
        else
        {
            isPullingCrate = false;
        }

    }

    private void SetRunningSpeed()
    {
        if (isPullingCrate)
        {
            animator.speed = runningSpeedWithCrate/defaultRunningSpeed;
            currentRunningSpeed = runningSpeedWithCrate;
        }
        else if(isTouchingLadder && !isTouchingGround)
        {
            animator.speed = 1f;
            currentRunningSpeed = runningSpeedOnLadder;
        }
        else
        {
            animator.speed = 1f;
            currentRunningSpeed = defaultRunningSpeed;
        }
    }
}
