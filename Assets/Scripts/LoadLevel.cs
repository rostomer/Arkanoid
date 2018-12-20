using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

    public int levelNum;

    public void LoadLevelFunction()
    {
        SceneManager.LoadScene(levelNum);
    }
}
