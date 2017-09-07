using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceScript : MonoBehaviour {

    public GameObject body;
    public GameObject tray;
    public ItemSwaying swayScript;
    public GameObject trayPos;
    public int playerNum = 0;

    // Use this for initialization
    void Start () {
        tray.GetComponent<ItemSpawner>().playerNum = playerNum;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
