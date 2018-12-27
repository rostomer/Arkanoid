using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class Ball : MonoBehaviour {

    public static float ballInitialVelocity = 600f;
    public GameObject childBurstParticlesObject;

    public GameObject birstPickUpParticles;

    public int damageDealt = 1;

    private Rigidbody rb = new Rigidbody();
    private Renderer rend;
    private Material originalmaterial;

    private Coroutine currentCoroutine = null;
    [HideInInspector]
    public bool ballInPlay = false;

    public static Ball instance = null;

    void Awake()
    {
        instance = this;

        rend = GetComponent<Renderer>();
        originalmaterial = rend.material;

        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("BirstBall") &&
            (gameObject.tag == "Ball" || gameObject.tag == "UpgradedBirstBall"
            || gameObject.tag == "UpgradedHeavyBall"))
        {
            Instantiate(birstPickUpParticles, transform.position, Quaternion.identity);
            BirstBonusPickUp(other);
        }
        if(other.gameObject.CompareTag("HeavyBall") &&
            (gameObject.tag == "Ball" || gameObject.tag == "UpgradedBirstBall")
            || gameObject.tag == "UpgradedHeavyBall")
        {
            HeavyBonusPickUp(other);
        }
    }

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        LaunchBall();

        if(Input.GetKeyDown(KeyCode.S))
        {
            rb.velocity = (new Vector3(ballInitialVelocity + 100f, -300f, 0f) * Time.deltaTime);
        }
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

    private void HeavyBonusPickUp(Collider other)
    {
        gameObject.tag = "UpgradedHeavyBall";

        Material heavyBallMaterial = other.gameObject.GetComponent<Renderer>().material;
        rend.material = heavyBallMaterial;

        damageDealt = 2;

        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(HeavyBonusPicked());
    }

    public int DealDamage()
    {
        return damageDealt;
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

    IEnumerator HeavyBonusPicked()
    {
        print(Time.time);

        yield return new WaitForSeconds(14f);

        gameObject.tag = "Ball";
        damageDealt = 1;
        print(Time.time);

        rend.material = originalmaterial;
    }
}
