using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] private float movementSpeed = 1f;
    private Rigidbody2D myRigidbody;
    bool isTimeToTurn = false;
    bool stopped = false;
    void Awake () {
        myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        if (stopped) { return; }
        myRigidbody.velocity = new Vector2(movementSpeed, myRigidbody.velocity.y);
        if (isTimeToTurn)
        {
            Turn();
        }
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTimeToTurn = true;
    }

    private void Turn()
    {
        transform.localScale = new Vector2(transform.localScale.x * (-1), transform.localScale.y);
        movementSpeed = movementSpeed *(-1f);
        isTimeToTurn = false;
    }

    public void Stop()
    {
        stopped = true;
    }


}
