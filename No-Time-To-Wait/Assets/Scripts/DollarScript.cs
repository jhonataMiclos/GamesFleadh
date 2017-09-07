using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollarScript : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        Invoke("delayedDestroy", 0.7f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += new Vector3(0f, 0.02f, 0f);
	}

    void delayedDestroy ()
    {
        Destroy(gameObject);
    }
}
