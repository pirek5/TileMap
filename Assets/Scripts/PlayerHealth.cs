using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Player {

    //config
    [SerializeField] private float verticalAfterHitBodyThrow = 1f;
    [SerializeField] private float horizontalAfterHitBodyThrow = 1f;
    [SerializeField] private float immortalityPeriod = 1f; 

    // state
    private int lives = 3;
    private bool immortality = false;
    private bool afterDeath = false;

    protected override void Update ()
    {
        if (isActive)
        {
            base.Update();
            LosingHealth();
        }

        if (afterDeath)
        {
            myRigidbody.velocity = Vector2.zero;
        }
	}

    private void LosingHealth() // TODO utrata zdrowia, a potem śmierć
    {
        if (IsPlayerTouching(bodyCollider, "Hazards") && !immortality)
        {
            StartCoroutine(TemporaryImmortality());
            lives--;
            myRigidbody.velocity = new Vector2(Mathf.Sign(myRigidbody.velocity.x)*horizontalAfterHitBodyThrow * -1f, verticalAfterHitBodyThrow);
            healthPanel.HeartLose();
        }
    }

    IEnumerator TemporaryImmortality()
    {
        isActive = false;
        immortality = true;
        yield return new WaitForSeconds(immortalityPeriod);
        if(lives <= 0)
        {
            DeathSequence();
        }
        else
        {
            immortality = false;
            isActive = true;
        }
    }

    private void DeathSequence()
    {
        afterDeath = true;
        isActive = false;
        animator.SetTrigger("Dead");
        GameManager.Instance.LevelLose();
    }
}
