using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

    public int heartIndex;
    private Animator animator;
    public bool isCollected = true;

	void Awake ()
    {
        animator = GetComponent<Animator>();
	}

    public void ChangeHealth()
    {
        isCollected = !isCollected;
        animator.SetBool("HealthCollected", isCollected);
    }	
}
