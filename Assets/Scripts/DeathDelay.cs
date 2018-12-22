using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDelay : MonoBehaviour
{
    public float deathDelay;
    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, deathDelay);
    }
}
