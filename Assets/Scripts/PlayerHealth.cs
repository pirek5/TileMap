using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Player { //TODO refaktoryzacja np. TempraryImmortality() odpowiedzlane za sprawdzenie czy postac zginełą? jakiś żart

    //config
    [SerializeField] private float verticalAfterHitBodyThrow = 1f;
    [SerializeField] private float horizontalAfterHitBodyThrow = 1f;
    [SerializeField] private float immortalityPeriod = 1f;
    [SerializeField] private float drowningTime = 1f;

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
            FallIntoTheWater();
        }

        if (afterDeath)
        {
            myRigidbody.velocity = Vector2.zero;
        }
	}

    private void LosingHealth()
    {
        if (IsPlayerTouching(bodyCollider, "Hazards") && !immortality)
        {
            StartCoroutine(TemporaryImmortality());
            lives--;
            myRigidbody.velocity = new Vector2(Mathf.Sign(myRigidbody.velocity.x)*horizontalAfterHitBodyThrow * -1f, verticalAfterHitBodyThrow);
            healthPanel.HeartLose(1);
        }
    }

    IEnumerator TemporaryImmortality()
    {
        isActive = false;
        immortality = true;
        yield return new WaitForSeconds(immortalityPeriod);
        if(lives <= 0)
        {
            afterDeath = true;
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
        animator.SetTrigger("Dead");
        GameManager.Instance.LevelLose();
    }

    private void FallIntoTheWater()
    {
        if (IsPlayerTouching(headCollider, "Water"))
        {
            healthPanel.HeartLose(3);
            isActive = false;
            afterDeath = true;
            lives = 0;
            myRigidbody.gravityScale = 0.3f;
            animator.SetBool("Running", false);
            animator.SetBool("OnLadder", true);
            animator.SetBool("Climbing", true);
            StartCoroutine(Drowning());
        }
    }

    private IEnumerator Drowning()
    {
        yield return new WaitForSeconds(drowningTime);
        DeathSequence();
    }
}
