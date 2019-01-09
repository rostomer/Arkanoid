using System.Collections;
using System.Collections.Generic;
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

        sortList.Sort((x, y) => y.Score - x.Score);

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
}
