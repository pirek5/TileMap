 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    //reference in editor
    [SerializeField] private PhysicsMaterial2D zeroFrictionMaterial;
    [SerializeField] private PhysicsMaterial2D afterDeathMaterial;

    //config
    [SerializeField] private float runningSpeedOnLadder = 1f;
    [SerializeField] private float runningSpeedWithCrate = 1f;
    [SerializeField] private float defaultRunningSpeed = 1f;
    [SerializeField] private float climbingLadderSpeed = 1f;
    [SerializeField] private float jumpingStrenght = 1f;
    [SerializeField] private float afterDeathBodyThrow = 1f;

    //state
    private float currentRunningSpeed;
    private bool isActive = true;
    public bool IsActive { set { isActive = value; } }
    private float xAxisInput, yAxisInput;
    private bool isTouchingGround;
    private GameObject crateToPull;
    private bool isPullingCrate;

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
        print(crateToPull);
        isTouchingGround = IsPlayerTouching(feetCollider, "Ground");
        xAxisInput = CrossPlatformInputManager.GetAxis("Horizontal");
        yAxisInput = CrossPlatformInputManager.GetAxis("Vertical");

        if (isActive)
        {
            PlayerMovement();
            if (crateToPull)
            {
                PullingCrate();
            }
            LosingHealth();
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
        if (isTouchingGround && !isPullingCrate)
        {
            transform.localScale = new Vector2(Mathf.Sign(xAxisInput), 1f); // Mathf.sign returns 1 or -1 - fliping sprite to runing direction
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
        bool isTouchingLadder = IsPlayerTouching(feetCollider, "Ladder");
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

    private void PullingCrate()
    {
        if (CrossPlatformInputManager.GetButton("Fire1"))
        {
            isPullingCrate = true;
            crateToPull.GetComponent<Rigidbody2D>().velocity = myRigidbody.velocity;
            currentRunningSpeed = runningSpeedWithCrate;
        }
        else
        {
            currentRunningSpeed = defaultRunningSpeed;
            crateToPull.GetComponent<Collider2D>().sharedMaterial = afterDeathMaterial;
            isPullingCrate = false;
        }

    }

    private void LosingHealth() // TODO utrata zdrowia, a potem śmierć
    {
        if (IsPlayerTouching(bodyCollider,"Hazards"))
        {
            DeathSequence();
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

    private bool IsPlayerTouching(Collider2D collider, string thing)
    {
        return collider.IsTouchingLayers(LayerMask.GetMask(thing));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Crates"))
        {
            collision.GetComponent<Collider2D>().sharedMaterial = zeroFrictionMaterial;
            crateToPull = collision.gameObject; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Crates"))
        {
            collision.GetComponent<Collider2D>().sharedMaterial = afterDeathMaterial;
            crateToPull = null;
        }
    }
}
