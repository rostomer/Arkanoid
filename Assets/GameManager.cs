using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Vector3 paddleSpawnCoorditates;
    public Vector3 paddleEulerQuaternian;
    public Vector3 BricksSpawnCoordinates;
    public int lives = 3;
    public int bricks = 20;
    public float resetDelay = 1f;
    public int sceneNum = 0;
    public float spawnChance = 0.5f;
    public Text livesText;
    public GameObject gameOver;
    public GameObject youWon;
    public GameObject brickPrefab;
    public GameObject newPaddle;
    public GameObject deathParticles;
    public GameObject BurstBall;
    public AudioSource brickHeat;
    public static GameManager instance = null;

    private GameObject cloneHadle;
	// Use this for initialization
	void Awake () {
        brickHeat = GetComponent<AudioSource>();

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        Setup();
	}

    public void ProduceSound()
    {
        brickHeat.Play();
    }

    public void Setup()
    {
        //hardcodeBlock
        //cloneHadle = Instantiate(newPaddle, transform.position, Quaternion.Euler(0f, 90f, 0f)) as GameObject;
        //Instantiate(brickPrefab, new Vector3(-2.3f, -4f, 0f), Quaternion.identity);
        //normalBlock
        cloneHadle = Instantiate(newPaddle, paddleSpawnCoorditates, Quaternion.Euler(paddleEulerQuaternian)) as GameObject;
        Instantiate(brickPrefab, BricksSpawnCoordinates, Quaternion.identity);
    }

    void CheckGameover()
    {
        if(bricks < 1)
        {
            youWon.SetActive(true);
            Time.timeScale = .25f;
            Invoke("Reset", resetDelay);
        }

        if(lives < 1)
        {
            gameOver.SetActive(true);
            Time.timeScale = .25f;
            Invoke("Reset", resetDelay);
        }
    }

    void Reset()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneNum);
    }

    public void LoseLife()
    {
        lives--;
        livesText.text = "Lives " + lives;

        Instantiate(deathParticles, cloneHadle.transform.position, Quaternion.identity);

        Destroy(cloneHadle);

        Invoke("SetupPaddle", resetDelay);

        CheckGameover();
    }

    void SetupPaddle()
    {
        cloneHadle = Instantiate(newPaddle, transform.position, Quaternion.Euler(paddleEulerQuaternian)) as GameObject;
    }

    public void DestroyBrick()
    {
        bricks--;

        CheckGameover();
    }
    
}
