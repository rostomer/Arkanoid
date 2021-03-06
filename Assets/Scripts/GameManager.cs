﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Vector3 paddleSpawnCoorditates;
    public Vector3 paddleEulerQuaternian;
    public Vector3 BricksSpawnCoordinates;

 //   public string NextLevel = "Level2";
    public int nextLevelUnlock = 2;

    public int lives = 3;   
    public float resetDelay = 1f;
    public int sceneNum = 0;
    public float spawnChance = 0.5f;

    public Text livesText;
    public Text BricksText;
    public Text scoreText;

    public GameObject pauseMenu;
    public GameObject gameOver;
    public GameObject youWon;
    public GameObject brickPrefab;
    public GameObject newPaddle;
    public GameObject deathParticles;
    public GameObject BurstBall;
    public GameObject HeavyBall;
    public AudioSource brickHeat;

    public static GameManager instance = null;

    public bool autoGeneration;

    public int bricksAmount = 0;

    private GameObject cloneHadle;
    [HideInInspector]
    public GameObject ball;
    [HideInInspector]
    public PlayerData previousLeaderData;

    [HideInInspector]
    public int bricksDestroyed = 0;
    [HideInInspector]
    public static int currentScore = 0;




    // Use this for initialization
    void Start () {
        if (Time.timeScale != 1f)
            Time.timeScale = 1f;

        brickHeat = GetComponent<AudioSource>();

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        scoreText.text = "Score: 0";

        Setup();

    }

    public void ProduceSound()
    {
        brickHeat.Play();
    }

    public void Setup()
    {
        cloneHadle = Instantiate(newPaddle, paddleSpawnCoorditates, Quaternion.Euler(paddleEulerQuaternian)) as GameObject;
        ball = GameObject.FindGameObjectWithTag("Ball");

        if (autoGeneration) return;

        Instantiate(brickPrefab, BricksSpawnCoordinates, Quaternion.identity);

        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");

        Debug.Log(bricks.Length);

        BricksText.text = "Bricks left: " + bricks.Length;

        instance.bricksAmount = bricks.Length;

        //List<int> temp = new List<int>();

        //for (int i = 0; i < 10; i++)
        //{
        //    temp.Add(i);
        //}

        //foreach(int k in temp)
        //{
        //    Debug.Log(k);
        //}

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    void CheckGameover()
    {
        if(autoGeneration && bricksAmount <= 0)
        {
            CreateLevel.instance.UplevelDifficalty();

            return;
        }

        if(bricksAmount <= 0)
        {
            youWon.SetActive(true);
            Time.timeScale = .25f;
            //storing the infirmation about reached level;
            PlayerPrefs.SetInt("levelReached", nextLevelUnlock);

            Invoke("Reset", resetDelay);
        }

        if(lives < 1)
        {
            gameOver.SetActive(true);
            Time.timeScale = .25f;
            Invoke("Reset", resetDelay);
        }

        if (lives < 1 && autoGeneration)
        {
            gameOver.SetActive(true);
            Time.timeScale = .25f;

            List<PlayerData> compareList = GameControl.instance.leaders;
         
                    GameControl.instance.leader.Score = currentScore;
                    GameControl.instance.leader.BricksDestroyed = bricksDestroyed;

                    GameControl.instance.Save();
      
            SceneManager.LoadScene("MainMenu");
        }


    }

    void Reset()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneNum);
    }

    public void LoseLife()
    {
        Debug.Log("LifeLost");
        lives--;
        livesText.text = "Lives " + lives;

        Instantiate(deathParticles, cloneHadle.transform.position, Quaternion.identity);

        Destroy(cloneHadle);
        Destroy(ball);

        Invoke("SetupPaddle", resetDelay);

        CheckGameover();
    }

    void SetupPaddle()
    {
        cloneHadle = Instantiate(newPaddle, transform.position, Quaternion.Euler(paddleEulerQuaternian)) as GameObject;
    }

    public void DestroyBrick(int brickPrice)
    {
        --bricksAmount;
        ++bricksDestroyed;

        currentScore += brickPrice;
        GameControl.instance.score = currentScore;

        UpdateUI();

        CheckGameover();
    }

    public void UpdateUI()
    {
        scoreText.text = "Score: " + currentScore;

        BricksText.text = "Bricks left: " + bricksAmount;

        Debug.Log("Bricks left: " + bricksAmount);
    }

    public void Pause()
    {
        try
        {
            ball = GameObject.FindGameObjectWithTag("UpgradedBirstBall");
        }
        catch(System.Exception e)
        {
            Debug.Log(e.ToString());
        }

        try
        {
            ball = GameObject.FindGameObjectWithTag("Ball");
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }

        cloneHadle.SetActive(false);

   //     ball.SetActive(false);

        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void Continue()
    {
        cloneHadle.SetActive(true);
     //   ball.SetActive(true);

        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
