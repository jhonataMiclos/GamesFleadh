using UnityEngine;
using System.Collections;

public class TrayRotation : MonoBehaviour 
{
    public Transform bodyTransform;
    public float rotatePrecent;
	public float torqueForceFixSpeed;
    public GameObject positionOfTray;
    public GameObject tray;

    // Use this for initialization
    void Start () 
	{
       
    }

	void FixedUpdate () // FixedUpdate is used for physics update
	{
        float bodyRotation = bodyTransform.eulerAngles.z;	// angle in degrees
		float trayRotation = transform.eulerAngles.z;		// angle of tray in degrees
		float appliedTorForce, desiredAngle;

		if (bodyRotation > 180)	// left side body angle modification (e.g. turns 350 into -10)
        {
            bodyRotation = bodyRotation - 360;
            //bodyRotation = -(360 - bodyRotation);
        }

		if (trayRotation > 180)	// left side tray angle modification (e.g. turns 350 into -10)
		{
            trayRotation = trayRotation - 360;
            //bodyRotation = bodyRotation;
        }

        desiredAngle = bodyRotation * rotatePrecent;	// angle which we want to position the tray in

		appliedTorForce = desiredAngle - trayRotation;	// torque force that will be applied onto the tray

		GetComponent<Rigidbody2D>().AddTorque(appliedTorForce * torqueForceFixSpeed * Time.deltaTime, ForceMode2D.Force);	// applies the rotational force
        
        //transform.eulerAngles = new Vector3(0, 0, bodyRotation * rotatePrecent);
        //tray.transform.position = positionOfTray.transform.position;

    }
}
