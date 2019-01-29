using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Player {

    //reference set in editor //TODO bez konieczności użycia edytora
    [SerializeField] private IconPanel healthPanel;

    //config
    [SerializeField] private float verticalAfterHitBodyThrow = 1f;
    [SerializeField] private float horizontalAfterHitBodyThrow = 1f;
    [SerializeField] private float inactivityPeriod = 1f;
    [SerializeField] private float drowningTime = 1f;

    // state
    private bool zeroVelocity = false;

    protected override void Update ()
    {
        if (isActive)
        {
            base.Update();
            LosingHealth();
            FallIntoTheWater();
        }

        if (zeroVelocity) // prevents weird behavior after death or during drowning
        {
            myRigidbody.velocity = Vector2.zero;
        }
	}

    private void LosingHealth()
    {
        if (IsPlayerTouching(bodyCollider, "Hazards") && isActive)
        {
            lives--;
            LevelScoreManager.Instance.UpdateHeartsAmount(lives);
            isActive = false;
            myRigidbody.velocity = new Vector2(Mathf.Sign(myRigidbody.velocity.x)*horizontalAfterHitBodyThrow * -1f, verticalAfterHitBodyThrow);
            healthPanel.IconDisable(1);
            if (lives <= 0)
            {
                StartCoroutine(DelayedDeath(inactivityPeriod));
            }
            else
            {
                StartCoroutine(TemporaryInactivity());
            }
        }
    }

    private void FallIntoTheWater()
    {
        if (IsPlayerTouching(headCollider, "Water"))
        {
            lives = 0;
            healthPanel.IconDisable(3);
            isActive = false;
            zeroVelocity = true;
            myRigidbody.gravityScale = 0.3f;
            animator.SetTrigger("Drowning");
            StartCoroutine(DelayedDeath(drowningTime));
        }
    }

    IEnumerator TemporaryInactivity()
    {
        yield return new WaitForSeconds(inactivityPeriod);
        isActive = true;
    }

    private IEnumerator DelayedDeath(float delay)
    {
        yield return new WaitForSeconds(delay);
        zeroVelocity = true;
        DeathSequence();
    }

    private void DeathSequence()
    {
        transform.localScale = new Vector2(1f, 1f);
        animator.SetTrigger("Dead");
        GameManager.Instance.LevelLose();
    }
}
