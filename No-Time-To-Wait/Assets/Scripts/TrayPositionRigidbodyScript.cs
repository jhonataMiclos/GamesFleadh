


using UnityEngine;
using System.Collections;

public class TrayPositionRigidbodyScript : MonoBehaviour {

	public Transform trayPosition;	// position where the tray should be
	public float power;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		transform.position = trayPosition.position;
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector2 offset = trayPosition.position - transform.position;

		offset.x = offset.x * Mathf.Abs (offset.x * power);
		offset.y = offset.y * Mathf.Abs (offset.y * power);

		Debug.Log (offset);
		rb.AddForce(offset);
	}
}
