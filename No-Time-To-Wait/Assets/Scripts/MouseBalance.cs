using UnityEngine;
using System.Collections;

public class MouseBalance : MonoBehaviour
{

    public float rotationSpeed;             // How sensitive the torque is applied
    public float rotationSpeedLimit;        // Fastest speed the body can rotate at
    public float rotationDeccelerationRate; // If no input is given, how fast players balancing input looses speed
    public string movementInput = "Horizontal_P1";

    private float rotationAcceleration;     // How much torque it's applying
    private float accelerationLimit;        // Max torque acceleration
    private float rotationDecceleration;
    private bool enableRotation;            // if false, horizontal axis movement won't apply rotational force

    // Use this for initialization
    void Start()
    {
        enableRotation = true;
        rotationAcceleration = 0;
        accelerationLimit = 10;
        rotationDecceleration = 0;
    }

    // Called every single physics update cycle
    public void FixedUpdate()
    {
        if (enableRotation)
        {
            applyRotation();
        }
    }

    private void applyRotation()
    {
        rotationAcceleration += Input.GetAxis(movementInput);
        
        rotationAcceleration = Mathf.Clamp(rotationAcceleration, -accelerationLimit, accelerationLimit);

        GetComponent<Rigidbody2D>().AddTorque(rotationAcceleration * rotationSpeed * Time.deltaTime);
        GetComponent<Rigidbody2D>().angularVelocity = Mathf.Clamp(GetComponent<Rigidbody2D>().angularVelocity, -rotationSpeedLimit, rotationSpeedLimit);

        if (Input.GetAxis(movementInput) == 0) // if mouse is not moved, deccelerate it
        {
            if (rotationAcceleration < -accelerationLimit * 0.1)    // if the acceleration is higher than 10% of the limit
            {
                rotationAcceleration += rotationDecceleration;
            }
            else if (rotationAcceleration > accelerationLimit * 0.1)
            {
                rotationAcceleration -= rotationDecceleration;
            }
            rotationDecceleration += rotationDeccelerationRate;
        }
        else
        {
            rotationDecceleration = 0;
        }
    }

    public void setEnableRotation(bool enableRotation)
    {
        this.enableRotation = enableRotation;
    }
}
