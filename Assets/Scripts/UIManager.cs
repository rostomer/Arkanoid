using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject leadersButton;
    public GameObject endlessModeButton;

    public InputField inputField;

    void Start()
    {
        try
        {
            if(GameControl.instance.leader.PlayerNickname != null)
            {
                inputField.text = GameControl.instance.leader.PlayerNickname;
                inputField.interactable = false;
            }
            else
            {
                inputField.text = "";
                inputField.interactable = true;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

	public void UIEnable()
    {
        if (inputField.text == "" && GameControl.instance.leader.PlayerNickname == null)
        {
            leadersButton.GetComponent<Button>().interactable = false;
            endlessModeButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            leadersButton.GetComponent<Button>().interactable = true;
            endlessModeButton.GetComponent<Button>().interactable = true;
        }
    }

    void Update()
    {
        UIEnable();
    }

        public void ReadNickName()
    {
        GameControl.instance.leader.PlayerNickname = inputField.text;

        inputField.interactable = false;
    }
}
