 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour { // base class - collect input and information about state of player

    //state
    static protected bool isActive;
    static public bool IsActive { set { isActive = value; } }
    protected float xAxisInput, yAxisInput;
    protected bool isTouchingGround;
    protected bool isPullingCrate;
    protected bool isTouchingLadder;
    protected GameObject crateToPull;

    //cached components 
    protected Animator animator;
    protected Rigidbody2D myRigidbody;
    protected BoxCollider2D feetCollider;
    protected CapsuleCollider2D bodyCollider;

    void Awake () {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
	}

    protected virtual void Update ()
    {
        isTouchingGround = IsPlayerTouching(feetCollider, "Ground");
        isTouchingLadder = IsPlayerTouching(feetCollider, "Ladder");
        xAxisInput = CrossPlatformInputManager.GetAxis("Horizontal");
        yAxisInput = CrossPlatformInputManager.GetAxis("Vertical");
	}

    protected bool IsPlayerTouching(Collider2D collider, string thing)
    {
        return collider.IsTouchingLayers(LayerMask.GetMask(thing));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Crates"))
        {
            crateToPull = collision.gameObject;
            crateToPull.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Crates"))
        {
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            crateToPull = null;
        }
    }
}
