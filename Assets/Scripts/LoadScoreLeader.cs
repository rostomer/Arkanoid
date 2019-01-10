using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScoreLeader : MonoBehaviour {

    public Text ScoreText;
    public GameObject ContentUI;

    public Text UITextToClone;

    // Use this for initialization
    void Start() {
        if (GameControl.instance.leaders != null)
            Debug.Log("Leaders here");

        List<PlayerData> sortList = GameControl.instance.leaders;

        GameControl.instance.leaders = GameControl.instance.leaders.OrderByDescending(playerData => playerData.Score).ToList();

        GameControl.instance.leaders = CheckListLength(GameControl.instance.leaders);

        CheckNewLeader();
	}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void CheckNewLeader()
    {
        foreach(PlayerData data in GameControl.instance.leaders)
        {
            UITextToClone.text = "Player: " + data.PlayerNickname + " Score: " + data.Score
                + " Bricks Destroyed: " + data.BricksDestroyed;

            Instantiate(UITextToClone, ContentUI.transform);
        }
    }

    private List<PlayerData> CheckListLength(List<PlayerData> data)
    {
        if(data.Count > 20)
        {
            for (int i = 20; i < data.Count; i++)
            {
                data.RemoveAt(i);
            }            
        }
        return data;
    }


    //test Function
 
}
