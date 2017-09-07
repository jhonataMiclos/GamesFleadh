using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump_P1") || Input.GetButtonDown("Jump_P2") || Input.GetButtonDown("Fire1"))
        {
            Debug.Log("all good");
            Application.LoadLevel("test zone");
        }
	}
}
