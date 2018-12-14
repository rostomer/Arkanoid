using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    public float speed = 1f;

    private GameObject leftWall;
    private GameObject rightWall;

    private float leftWall_X_Coordinate;
    private float rightWall_X_Coordinate;

    public Vector3 playerPosition = new Vector3(0, -9.5f, 0);
	
    public void Start()
    {
        leftWall = GameObject.FindGameObjectWithTag("LeftWall");
        rightWall = GameObject.FindGameObjectWithTag("RightWall");

        leftWall_X_Coordinate = leftWall.transform.position.x;
        rightWall_X_Coordinate = rightWall.transform.position.x;

        Debug.Log("Left Wall: " + leftWall_X_Coordinate + "Right Wall: " + rightWall_X_Coordinate);
    }

	// Update is called once per frame
	void Update () {
        float xPos = transform.position.x + Input.GetAxis("Horizontal") * speed;
        playerPosition = new Vector3(Mathf.Clamp(xPos, leftWall_X_Coordinate + 3.2f,
              rightWall_X_Coordinate - 3.2f), -9.5f, 0);
        transform.position = playerPosition;
	}
}
