using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using System.Linq;

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

        //leader = null;

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

        bool existingPlayer = false;

        foreach(PlayerData dat in leaders)
        {
            if(dat.PlayerNickname == leader.PlayerNickname)
            {
                existingPlayer = true;
            }
        }
            if(!existingPlayer)
                leaders.Add(leader);
            else if(existingPlayer)
                {
                    leaders = RemoveExistingPlayerWithLessScore(leaders);
                    leaders.Add(leader);
        }
            
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

    //Bad Linq using
    //private List<PlayerData> CheckEqualValues(List<PlayerData> data)
    //{
    //    var newData = from pickData in data
    //                  where pickData.PlayerNickname == leader.PlayerNickname
    //                  orderby pickData.Score
    //                  select pickData;

    //    data = newData.ToList();

    //    Debug.Log("Type: " + newData.GetType());

    //    return data;
    //}

    private List<PlayerData> RemoveExistingPlayerWithLessScore(List<PlayerData> data)
    {
        data.RemoveAll(x => x.PlayerNickname == leader.PlayerNickname && x.Score < leader.Score);
        return data;
    }
}

