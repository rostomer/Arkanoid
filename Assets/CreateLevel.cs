using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevel : MonoBehaviour {

    public GameObject spawnPrefab;
    public GameObject block;
    public int blocksAmount;
    public GameObject durableBlock;
    public int DurableBlockAmount;
    public GameObject veryDurableBlock;
    public int veryDurableBlockAmount;

    private Vector3 firstBrickPos;
    private Vector3 leftWallPos;
    private Vector3 rightWallPos;

    private float startXPos;


    Vector3 originPoint;

    void Awake()
    {
        leftWallPos = GameObject.FindGameObjectWithTag("LeftWall").transform.position;
        rightWallPos = GameObject.FindGameObjectWithTag("RightWall").transform.position;

        startXPos = leftWallPos.x + 1.5f;

        firstBrickPos = new Vector3(startXPos, 7.5f, 0f);

        CreateGroup();

    }

    void CreateGroup()
    {
        originPoint = spawnPrefab.gameObject.transform.position;

        for (int i = 0; i < blocksAmount; i++)
        {
            CreateAgent();
        }


    }

    public void CreateAgent()
    {
        float directionFacing = Random.Range(0f, 360f);

        Vector3 point = firstBrickPos;

        Instantiate(block, point, Quaternion.identity);

        point = new Vector3(point.x + 3f, point.y, 0);

        if (point.x >= rightWallPos.x)
        {
            point.y -= 1.3f;
            point.x = startXPos;
        }

        firstBrickPos = point;
    }
}
