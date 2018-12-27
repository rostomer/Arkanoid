using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adjust : MonoBehaviour {

    void OnGUI()
        {
        if (GUI.Button(new Rect(10, 100, 100, 30), "Score UP"))
        {
            GameControl.control.score += 10;
        }

        if (GUI.Button(new Rect(10, 180, 100, 30), "Lifes Down"))
        {
            GameControl.control.lifes -= 1;
        }

        if (GUI.Button(new Rect(10, 220, 100, 30), "Lifes Up"))
        {
            GameControl.control.lifes += 1;
        }

        if (GUI.Button(new Rect(10, 260, 100, 30), "Save"))
        {
            GameControl.control.Save();
        }

        if (GUI.Button(new Rect(10, 300, 100, 30), "Load"))
        {
            GameControl.control.Load();
        }

    }
}
