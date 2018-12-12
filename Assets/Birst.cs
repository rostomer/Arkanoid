using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birst : MonoBehaviour {
    private float birstTime = 0.03f;

    private Collider birstZone;
    // используй в качестве тиггера для срабатывания коллайдера
    public static bool isBirst = false;

    private float timeCounter = 0f;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Brick"))
        {
            Debug.Log("Hit");
      //      Bricks.instance.BrickTakesDamage();
       //     birstZone.enabled = true;
        }
    }
	// Use this for initialization
	void Start () {
        birstZone = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isBirst)
            birstZone.enabled = true;
        else
            birstZone.enabled = false;
       
        if (birstZone.enabled)
        {
            //    Debug.Log("time " + timeCounter);

            timeCounter += Time.deltaTime;
        }
        if (timeCounter >= birstTime)
        {
            birstZone.enabled = false;
            timeCounter = 0;
        }

        if (birstZone == null)
            return;

    }
}
