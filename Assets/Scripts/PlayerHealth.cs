using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Player {
    //reference in editor
    [SerializeField] protected PhysicsMaterial2D zeroFrictionMaterial;
    [SerializeField] protected PhysicsMaterial2D afterDeathMaterial;

    //config
    [SerializeField] private float afterDeathBodyThrow = 1f;

    protected override void Update ()
    {
        if (isActive)
        {
            base.Update();
            LosingHealth();
        }
	}

    private void LosingHealth() // TODO utrata zdrowia, a potem śmierć
    {
        if (IsPlayerTouching(bodyCollider, "Hazards"))
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
}
