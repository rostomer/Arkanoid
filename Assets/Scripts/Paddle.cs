using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    public float speed = 1f;

    public Vector3 playerPosition = new Vector3(0, -9.5f, 0);
	
	// Update is called once per frame
	void Update () {
        float xPos = transform.position.x + Input.GetAxis("Horizontal") * speed;
        playerPosition = new Vector3(Mathf.Clamp(xPos, -7.3f, 7.3f), -9.5f, 0);
        transform.position = playerPosition;
	}
}
