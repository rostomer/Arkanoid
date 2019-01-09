using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject leadersButton;
    public GameObject endlessModeButton;

    public InputField inputField;

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
    }
}
