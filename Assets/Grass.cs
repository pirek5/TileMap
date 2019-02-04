using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour {

    [SerializeField] GrassCollider leftCollider;
    [SerializeField] GrassCollider rightCollider;
    Animator animator;
    Collider2D collisionCollider;

    bool leftSideTouched, rightSideTouched;
    bool collisionIsMoving = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update () {
        GetData();
        AnimateGrass();    
    }

    private void GetData()
    {
        leftSideTouched = leftCollider.isTriggerd;
        rightSideTouched = rightCollider.isTriggerd;
        if (rightCollider.collisionRigidbody2D)
        {
            collisionIsMoving = rightCollider.collisionRigidbody2D.velocity != Vector2.zero;
        }
        else
        {
            collisionIsMoving = false;
        }

        if (leftCollider.collisionRigidbody2D)
        {
            collisionIsMoving = leftCollider.collisionRigidbody2D.velocity != Vector2.zero;
        }
        else
        {
            collisionIsMoving = false;
        }

    }

    private void AnimateGrass()
    {
        animator.SetBool("right", rightSideTouched && collisionIsMoving);
        animator.SetBool("left", leftSideTouched && collisionIsMoving);
    }

    //[SerializeField] BoxCollider2D leftCollider;
    //[SerializeField] BoxCollider2D rightCollider;
    //Animator animator;
    //Collider2D collisionCollider;

    //bool leftSideTouched, rightSideTouched, playerIsMooving;

    //private void Awake()
    //{
    //    animator = GetComponent<Animator>();
    //}

    //void Update()
    //{
    //    GetData();
    //    AnimateGrass();
    //}

    //private void GetData()
    //{
    //    leftSideTouched = leftCollider.IsTouchingLayers(LayerMask.GetMask("Player"));
    //    rightSideTouched = rightCollider.IsTouchingLayers(LayerMask.GetMask("Player"));
    //    playerIsMooving = Player.xAxisInput != 0;
    //}

    //private void AnimateGrass()
    //{
    //    animator.SetBool("right", rightSideTouched && playerIsMooving);
    //    animator.SetBool("left", leftSideTouched && playerIsMooving);
    //}
}
