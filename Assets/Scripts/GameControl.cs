using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    public static GameControl instance;

    public string nickname;
    public int score;
    public int bricksDestroyed;

    [HideInInspector]
    public PlayerData leader = null;
    [HideInInspector]
    public List<PlayerData> leaders;
    [HideInInspector]
    public PlayerData oldLeaders = null;
    

    void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        Load();
       // leaders = Load();
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 150, 30), "Score: " + score);
        GUI.Label(new Rect(10, 40, 150, 30), "Bricks destroyed: " + bricksDestroyed);
        GUI.Label(new Rect(10, 70, 150, 30), "Nickname: " + nickname);
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();

            data.Score = leader.Score;
            data.PlayerNickname = leader.PlayerNickname;
            data.BricksDestroyed = leader.BricksDestroyed;
       
                leaders.Add(leader);
     
        if (leaders != null)
            Debug.Log("Added");
         
        bf.Serialize(file, leaders);
        file.Close();
    }

    public void Load()
    {
      
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            leaders = (List<PlayerData>)bf.Deserialize(file);
            if (leaders != null)
                Debug.Log("Loaded");
            file.Close();         
        }
    }
}

