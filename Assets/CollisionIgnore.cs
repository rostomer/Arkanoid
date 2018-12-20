using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionIgnore : MonoBehaviour {

    private static Collider coll;

    void Start()
    {
        coll = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball") || other.gameObject.CompareTag("UpgradedBirstBall"))
        {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    public static void Ignore()
    {
        Physics.IgnoreCollision(coll,
           GameObject.FindGameObjectWithTag("UpgradedBirstBall").GetComponent<Collider>());
    }
	
}
