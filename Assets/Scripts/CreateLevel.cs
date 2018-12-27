using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevel : MonoBehaviour {

    public GameObject spawnPrefab;
    public GameObject block;
    public int blocksAmount;
    public GameObject durableBlock;
    public GameObject veryDurableBlock;

    private Vector3 firstBrickPos;
    private Vector3 leftWallPos;
    private Vector3 rightWallPos;

    private float spawnXPos;
    private float startSpawnXPos;
    private float startSpawnYPos;
    private Vector3 point;

    public static CreateLevel instance = null;

    Vector3 originPoint;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        leftWallPos = GameObject.FindGameObjectWithTag("LeftWall").transform.position;
        rightWallPos = GameObject.FindGameObjectWithTag("RightWall").transform.position;
        startSpawnYPos = GameObject.FindGameObjectWithTag("Roof").transform.position.y - 2f;

        spawnXPos = leftWallPos.x + 3f;
        startSpawnXPos = spawnXPos;

        firstBrickPos = new Vector3(spawnXPos, startSpawnYPos, 0f);

        CreateGroupOfBlocks();

    }

    public void UplevelDifficalty()
    {
        blocksAmount += 2;

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Brick"))
        Destroy(obj);

        CreateGroupOfBlocks();

        DestroyBeforeUpgrade();

        Debug.Log("BlockAmount: " + blocksAmount);

        GameManager.instance.Setup();

        Ball.ballInitialVelocity += 20;

        SearchForBricks();
    }

    private void SearchForBricks()
    {
       // GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");

        GameManager.instance.bricksAmount = blocksAmount;

        Debug.Log(GameManager.instance.bricksAmount);
    }

    void Start()
    {
        SearchForBricks();

        GameManager.instance.BricksText.text = "Bricks left: " + blocksAmount;
    }

    void CreateGroupOfBlocks()
    {
        firstBrickPos = new Vector3(spawnXPos, startSpawnYPos, 0f);

        for (int i = 0; i < blocksAmount; i++)
        {
            SpawnBlock();
        }
    }

    private void DestroyBeforeUpgrade()
    {
        try
        {
            Destroy(GameManager.instance.ball);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

        try
        {
            Destroy(GameObject.FindGameObjectWithTag("BirstBall"));
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
        try
        {
            Destroy(GameObject.FindGameObjectWithTag("HeavyBall"));
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
        try
        {
            Destroy(GameObject.FindGameObjectWithTag("Ball"));
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
        try
        {
            Destroy(GameObject.FindGameObjectWithTag("UpgradedHeavyBall"));
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
        try
        {
            Destroy(GameObject.FindGameObjectWithTag("UpgradedBirstBall"));
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }


        Destroy(GameObject.FindGameObjectWithTag("Player"));
    }

    public void SpawnBlock()
    {

        GameObject blockForSpawn = block;

        int BlockType = UnityEngine.Random.Range(1, 4);

        switch (BlockType)
        {
            case 1:
                blockForSpawn = block;
                Debug.Log("block has spawned");
                break;
            case 2:
                blockForSpawn = durableBlock;
                Debug.Log("Durable block has spawned");
                break;
            case 3:
                blockForSpawn = veryDurableBlock;
                Debug.Log("Very durable block has spawned");
                break;
        }

        point = firstBrickPos;

        Instantiate(blockForSpawn, point, Quaternion.identity);

        point = new Vector3(point.x + 3f, point.y, 0);

        if (point.x >= rightWallPos.x - 1f)
        {
            point.y -= 1.5f;
            point.x = spawnXPos;
        }

        firstBrickPos = point;
    }
}
