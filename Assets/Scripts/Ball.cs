using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float ballInitialVelocity = 600f;
    

    private Rigidbody rb = new Rigidbody();
    private bool ballInPlay = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("BirstBall"))
        {
            Destroy(other.gameObject);
        }
    }

	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1") && ballInPlay == false)
        {
            Debug.Log("Fire1");
            transform.parent = null;
            ballInPlay = true;
            rb.isKinematic = false;
            rb.AddForce(new Vector3(ballInitialVelocity, ballInitialVelocity, 0));
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Brick")
        {
            GameManager.instance.ProduceSound();
        }
    }
}
