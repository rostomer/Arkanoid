using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class Ball : MonoBehaviour {

    public float ballInitialVelocity = 600f;
    public GameObject childBurstParticlesObject;

    private Rigidbody rb = new Rigidbody();
    private Renderer rend;
    private bool ballInPlay = false;

    void Awake()
    {        
        rend = GetComponent<Renderer>();

        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("BirstBall"))
        {
            Material birstBallMaterial = other.gameObject.GetComponent<Renderer>().material;

            childBurstParticlesObject.SetActive(true);
            ParticleSystem birstParticles = childBurstParticlesObject.GetComponent<ParticleSystem>();
            birstParticles.Play();
            rend.material = birstBallMaterial;
          //  Instantiate(ps, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }

	// Use this for initialization
	
	
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
