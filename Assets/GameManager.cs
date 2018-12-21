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

    public bool autoGeneration;

    [HideInInspector]
    public int bricksAmount = 0;

    private GameObject cloneHadle;
	// Use this for initialization
	void Start () {
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
        cloneHadle = Instantiate(newPaddle, paddleSpawnCoorditates, Quaternion.Euler(paddleEulerQuaternian)) as GameObject;

        if (autoGeneration) return;

        Instantiate(brickPrefab, BricksSpawnCoordinates, Quaternion.identity);

        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");

        Debug.Log(bricks.Length);

        instance.bricksAmount = bricks.Length;
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
        Debug.Log("LifeLost");
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
       // bricksAmount--;

        Debug.Log("Bricks left: " + bricksAmount);

        CheckGameover();
    }
    
}
