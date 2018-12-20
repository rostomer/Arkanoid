using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birst : MonoBehaviour {

    private Collider birstZone;
    // используй в качестве тиггера для срабатывания коллайдера
    public static bool isBirst = false;

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Brick"))
    //    {
    //        Debug.Log("Hit");
    //  //      Bricks.instance.BrickTakesDamage();
    //   //     birstZone.enabled = true;
    //    }
    //}
	// Use this for initialization
	void Start () {
        birstZone = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {

        if(IsBirst())
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("BirstBall"))
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), obj.GetComponent<Collider>());
            }

            StartCoroutine(Wait());
        }

        if (birstZone == null)
            return;

    }

    private bool IsBirst()
    {
        if (isBirst)
        {
         //   Debug.Log("Birst");
            birstZone.enabled = true;
            return true;

        }
        else
        {
            birstZone.enabled = false;
            return false;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.02f);

        isBirst = false;
    }
}
