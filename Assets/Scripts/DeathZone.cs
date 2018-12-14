using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball") || other.gameObject.CompareTag("UpgradedBirstBall"))
        GameManager.instance.LoseLife();
    }

}
