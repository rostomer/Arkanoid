using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour {

    public GameObject brickParticle;
    public int durabilityPoints;
    public static AudioSource contactWithBall;

    public int brickPrice;

    public static Bricks instance = null;

    private Renderer rend;

    void Start()
    {
        instance = this;

        rend = GetComponent<Renderer>();
        contactWithBall = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball") || other.gameObject.CompareTag("UpgradedHeavyBall"))
        {
            BrickTakesDamage();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("UpgradedBirstBall"))
        {
            BrickTakesDamage();
        }
    }

    public void BrickTakesDamage()
    {
        durabilityPoints -= Ball.instance.DealDamage();

        if (durabilityPoints <= 0)
        {
            if (Random.value <= GameManager.instance.spawnChance)
            {
                float chance = Random.Range(0f, 10f);
                if (chance <= 5f)
                {
                    Debug.Log(chance);
                    Instantiate(GameManager.instance.BurstBall, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(GameManager.instance.HeavyBall, transform.position, Quaternion.identity);
                    Debug.Log(chance);
                }
                    GameManager.instance.spawnChance = 0.05f;
            }
            else
            {
                GameManager.instance.spawnChance += 0.01f;
            }

            GameControl.instance.leader.BricksDestroyed++;
            GameManager.instance.DestroyBrick(brickPrice);

            Instantiate(brickParticle, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }

        rend.material.SetColor("_Color", Random.ColorHSV());
    }
}
