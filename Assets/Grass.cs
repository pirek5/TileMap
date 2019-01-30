using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour {

    [SerializeField] BoxCollider2D leftCollider;
    [SerializeField] BoxCollider2D rightCollider;
    Animator animator;

    bool left, right, isPlayerMooving;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        GetData();
        AnimateGrass();    
    }

    private void GetData()
    {
        left = leftCollider.IsTouchingLayers(LayerMask.GetMask("Player"));
        right = rightCollider.IsTouchingLayers(LayerMask.GetMask("Player"));
        isPlayerMooving = Player.xAxisInput != 0;
    }

    private void AnimateGrass()
    {
        animator.SetBool("left", right && isPlayerMooving);
        animator.SetBool("right", left && isPlayerMooving);
    }

}
