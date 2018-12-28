using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adjust : MonoBehaviour {

    void OnGUI()
        {
        if (GUI.Button(new Rect(10, 100, 100, 30), "Score UP"))
        {
            GameControl.instance.score += 10;
        }

        if (GUI.Button(new Rect(10, 260, 100, 30), "Save"))
        {
            GameControl.instance.Save();
        }

        if (GUI.Button(new Rect(10, 300, 100, 30), "Load"))
        {
            GameControl.instance.Load();
        }

    }
}
