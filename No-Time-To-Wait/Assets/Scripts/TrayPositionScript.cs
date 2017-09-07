using UnityEngine;
using System.Collections;

public class TrayPositionScript : MonoBehaviour {

	public Transform trayPosition;	// position where the tray should be

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = trayPosition.position;
        //transform.rotation = trayPosition.rotation;
    }
}
