 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour { // base class - collect input and information about state of player

    //state
    protected int lives = 3;
    static protected bool isActive;
    static public bool IsActive { get { return isActive; }  set { isActive = value; } }
    public static float xAxisInput, yAxisInput;
    protected bool isTouchingGround, isTouchingEnemy, isHeadTouchingLava, isTouchingWater, isFeetTouchingLava, isTouchingLadder, isHoldingCrate;

    //cached components 
    protected Animator animator;
    protected Rigidbody2D myRigidbody;
    protected BoxCollider2D feetCollider;
    protected CapsuleCollider2D bodyCollider;
    protected CircleCollider2D headCollider;

    void Awake () {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        headCollider = GetComponent<CircleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();

	}

    protected virtual void Update ()
    {
        isTouchingGround = IsPlayerTouching(feetCollider, "Ground") || IsPlayerTouching(feetCollider, "Crates");
        isTouchingLadder = IsPlayerTouching(feetCollider, "Ladder");
        isFeetTouchingLava = IsPlayerTouching(feetCollider, "Lava");
        isHeadTouchingLava = IsPlayerTouching(headCollider, "Lava");
        isTouchingWater = IsPlayerTouching(headCollider, "DeepWater");
        isTouchingEnemy = IsPlayerTouching(bodyCollider, "Hazards");
        xAxisInput = CrossPlatformInputManager.GetAxis("Horizontal");
        yAxisInput = CrossPlatformInputManager.GetAxis("Vertical");
	}

    protected bool IsPlayerTouching(Collider2D collider, string thing)
    {
        return collider.IsTouchingLayers(LayerMask.GetMask(thing));
    }
}
