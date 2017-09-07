using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawnerScript : MonoBehaviour {

    public GameObject cloud;
    public float respawnTime = 5f;

    private float timer;

	// Use this for initialization
	void Start () {
        timer = respawnTime;
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            GameObject cloudInstance = Instantiate(cloud);
            cloud.transform.position = transform.position;

            timer = respawnTime;
        }
    }
}
