﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour {

    public GameObject brickParticle;
    public int durabilityPoints;
    public static AudioSource contactWithBall;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        contactWithBall = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        durabilityPoints--;

        if (durabilityPoints <= 0)
        {
            if(Random.value <= GameManager.instance.spawnChance)
            {
                Instantiate(GameManager.instance.BurstBall, transform.position, Quaternion.identity);

                GameManager.instance.spawnChance = 0.05f;
            }
            else
            {
                GameManager.instance.spawnChance += 0.01f;
            }
       
            Instantiate(brickParticle, transform.position, Quaternion.identity);
            GameManager.instance.DestroyBrick();
            Destroy(gameObject);
        }
    
        rend.material.SetColor("_Color", Random.ColorHSV());
        
    }



}
