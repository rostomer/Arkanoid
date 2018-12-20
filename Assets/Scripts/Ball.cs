using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class Ball : MonoBehaviour {

    public float ballInitialVelocity = 600f;
    public GameObject childBurstParticlesObject;

    public GameObject birstPickUpParticles;

    private Rigidbody rb = new Rigidbody();
    private Renderer rend;
    private Material originalmaterial;

    private Coroutine currentCoroutine = null;

    private bool ballInPlay = false;

    void Awake()
    {  

        rend = GetComponent<Renderer>();
        originalmaterial = rend.material;

        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("BirstBall") &&
            (gameObject.tag == "Ball" || gameObject.tag == "UpgradedBirstBall"))
        {
            Instantiate(birstPickUpParticles, transform.position, Quaternion.identity);
            BirstBonusPickUp(other);
        }
    }

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        LaunchBall();

    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Brick")
        {
            Birst.isBirst = true;

            GameManager.instance.ProduceSound();

          //  Birst.isBirst = false;
        }
    }

    private void BirstBonusPickUp(Collider other)
    {
        gameObject.tag = "UpgradedBirstBall";

        Material birstBallMaterial = other.gameObject.GetComponent<Renderer>().material;

        childBurstParticlesObject.SetActive(true);

        ParticleSystem birstParticles = childBurstParticlesObject.GetComponent<ParticleSystem>();
        birstParticles.Play();

        rend.material = birstBallMaterial;

        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(ExplosionTime());

        //  Instantiate(ps, transform.position, Quaternion.identity);

    }

    private void LaunchBall()
    {
        if (Input.GetButtonDown("Fire1") && ballInPlay == false)
        {
            Debug.Log("Fire1");

            transform.parent = null;
            ballInPlay = true;
            rb.isKinematic = false;
            rb.velocity = (new Vector3(ballInitialVelocity, ballInitialVelocity, 0) * Time.deltaTime);

            GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");

            GameManager.instance.bricksAmount = bricks.Length;

            Debug.Log(GameManager.instance.bricksAmount);
        }
    }

    IEnumerator ExplosionTime()
    {
         print(Time.time);

        Physics.IgnoreCollision(GetComponent<Collider>(),
    GameObject.FindGameObjectWithTag("BirstBall").GetComponent<Collider>());

        yield return new WaitForSeconds(10f);

        gameObject.tag = "Ball";
         print(Time.time);

        childBurstParticlesObject.SetActive(false);

        rend.material = originalmaterial;
    }
}
