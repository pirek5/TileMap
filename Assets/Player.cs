 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    //reference in editor
    [SerializeField] private PhysicsMaterial2D afterDeathMaterial;

    //config
    [SerializeField] private float runningSpeedOnLadder = 1f;
    [SerializeField] private float defaultRunningSpeed = 1f;
    [SerializeField] private float climbingLadderSpeed = 1f;
    [SerializeField] private float jumpingStrenght = 1f;
    [SerializeField] private float afterDeathBodyThrow = 1f;

    //state
    private float currentRunningSpeed;
    private bool isActive = true;
    public bool IsActive { set { isActive = value; } }
    private float xAxisInput, yAxisInput;
    bool isTouchingGround;

    //cached components 
    private Animator animator;
    private Rigidbody2D myRigidbody;
    private BoxCollider2D feetCollider;
    private CapsuleCollider2D bodyCollider;

    void Awake () {
        currentRunningSpeed = defaultRunningSpeed;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
	}
	
	void Update ()
    {
        isTouchingGround = feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        xAxisInput = CrossPlatformInputManager.GetAxis("Horizontal");
        yAxisInput = CrossPlatformInputManager.GetAxis("Vertical");

        if (isActive)
        {
            PlayerMovement();
            if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
            {
                DeathSequence();
            }
        }
	}

    private void PlayerMovement()
    {
        Running();
        Jumping();
        ClimbingLadder();
    }

    private void Running()
    {
        bool isRunning = Mathf.Abs(xAxisInput) > Mathf.Epsilon;
        animator.SetBool("Running", isRunning);  // running animation
        myRigidbody.velocity = new Vector2(xAxisInput * currentRunningSpeed, myRigidbody.velocity.y);
        if (isTouchingGround)
        {
            transform.localScale = new Vector2(Mathf.Sign(xAxisInput), 1f); // Mathf.sign returns 1 or -1 - fliping sprite to runing direction
        }
    }

    private void Jumping()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump") && isTouchingGround)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpingStrenght);
        }
    }

    private void ClimbingLadder()
    {
        bool isTouchingLadder = feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));
        animator.SetBool("OnLadder", isTouchingLadder && !isTouchingGround);
        animator.SetBool("Climbing", isTouchingLadder && (yAxisInput != 0 || xAxisInput != 0));

        if (isTouchingLadder && !isTouchingGround)
        {
            currentRunningSpeed = runningSpeedOnLadder;
            myRigidbody.gravityScale = 0;
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, yAxisInput * climbingLadderSpeed);
        }
        else
        {
            currentRunningSpeed = defaultRunningSpeed;
            myRigidbody.gravityScale = 1;
        }
    }

    private void DeathSequence()
    {
        isActive = false;
        animator.SetTrigger("Dead");
        myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y + afterDeathBodyThrow);
        feetCollider.sharedMaterial = afterDeathMaterial;
        bodyCollider.sharedMaterial = afterDeathMaterial;
        GameManager.Instance.LevelLose();
    }
}
