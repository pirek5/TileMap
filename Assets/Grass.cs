using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour {

    [SerializeField] BoxCollider2D leftCollider;
    [SerializeField] BoxCollider2D rightCollider;
    Animator animator;

    bool leftSideTouched, rightSideTouched, playerIsMooving;

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
        leftSideTouched = leftCollider.IsTouchingLayers(LayerMask.GetMask("Player"));
        rightSideTouched = rightCollider.IsTouchingLayers(LayerMask.GetMask("Player"));
        playerIsMooving = Player.xAxisInput != 0;
    }

    private void AnimateGrass()
    {
        animator.SetBool("right", rightSideTouched && playerIsMooving);
        animator.SetBool("left", leftSideTouched && playerIsMooving);
    }

}
