using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birst : MonoBehaviour {

    private Collider birstZone;
    // используй в качестве тиггера для срабатывания коллайдера
    public static bool isBirst = false;

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

        if(IsBirst())
        {
            StartCoroutine(ExplosionTime());
            isBirst = false;
        }

        if (birstZone == null)
            return;

    }

    private bool IsBirst()
    {
        if (isBirst)
        {
            Debug.Log("Birst");
            birstZone.enabled = true;
            return true;

        }
        else
        {
            birstZone.enabled = false;
            return false;
        }
    }

    IEnumerator ExplosionTime()
    {
       // print(Time.time);
        yield return new WaitForSeconds(2f);
       // print(Time.time);
    }
}
