﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour {

    public Button[] levelButtons;

    //unlocking levels if they are reached in arcade mode;

	// Use this for initialization
	void Start () {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                Debug.Log("Button Blocked: " + "Button " + (i + 1));
                levelButtons[i].interactable = false;
            }

        }
	}
	
}
