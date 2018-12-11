using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    private void OnTriggerEnter()
    {
        GameManager.instance.LoseLife();
    }

}
