using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovementScript : MonoBehaviour {

    public float deleteTime = 5f;

    private float timer;

	// Use this for initialization
	void Start () {
        timer = deleteTime;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(0.015f, 0, 0);
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            Destroy(gameObject);
        }
	}
}
