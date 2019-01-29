using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {

    [SerializeField] private float lavaSpeed = 0f;
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector2(transform.position.x, transform.position.y + lavaSpeed * Time.deltaTime);
	}
}
