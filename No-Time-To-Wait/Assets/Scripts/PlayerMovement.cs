using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float tiltSpeed; // How fast the player moves based on the angle
    public MouseBalance bodyMouseBalanceScript;
    public GameObject bodyGameObject;
    public Transform bodyTransform;
    private Rigidbody2D rb;
    public float maxSpeed = 3f;
    public float moveSpeed;
    public float jumpPower;
    public bool standing;
    public bool enableFalling;
    public bool grounded;
    public string jumpInput = "Jump_P1";
    public float maxAnimSpeed = 5f;
    public Animator bodyAnim;
    public int powerUpNum = 0;

    private int playerNum;
    private Animator legAnim;
    private float maxBodyAngle;
    private MouseBalance mouseBalanceScript;
    private Vector2 legRespawnPosition;     // Position where the legs will respawn after falling down
    private bool fallingExecuted;
    private Vector2 initBodyPosTrans;       // How much you need to add to leg position, to arrive at the initial body position

    private SpriteRenderer mySpriteRenderer; 

    // Use this for initialization
    void Start () {
        mySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        standing = true;
        rb = GetComponent<Rigidbody2D>();
        maxBodyAngle = Mathf.Abs(bodyGameObject.GetComponent<HingeJoint2D>().limits.max);   // gets the positive angle at which the body falls over
        mouseBalanceScript = bodyGameObject.GetComponent<MouseBalance>();
        initBodyPosTrans = bodyTransform.position - transform.position;                     // How much you need to add to leg position, to arrive at the initial body position
        legAnim = GetComponent<Animator>();
        playerNum = GetComponent<ReferenceScript>().playerNum;
    }
    
	
	// Update is called once per frame
	void FixedUpdate () {
        float bodyRotation = bodyTransform.eulerAngles.z;   // Record the rotation of the body

        if (bodyRotation > 180) // Right side tilting of the body
        {
            bodyRotation = 360 - bodyRotation;
        }
        else                    // Left side tilting of the body
        {
            bodyRotation = -(bodyRotation);
        }

        if (standing && enableFalling)
        {
            standing = Mathf.Abs(bodyRotation) < maxBodyAngle;  // False if body is tilting beyond max body angle
        }

        if (standing)
        {
            applyMovement(bodyRotation);    // Move the character

            // Change facial expression on waiter's face
            if(Mathf.Abs(bodyRotation) < 30)
            {
                bodyAnim.SetInteger("facialNum", 0);
            }
            else if (Mathf.Abs(bodyRotation) < 50)
            {
                bodyAnim.SetInteger("facialNum", 1);
            }
            else
            {
                bodyAnim.SetInteger("facialNum", 2);
            }

            if (playerNum == 1)
            {
                legAnim.SetFloat("speedMult", (bodyRotation / (maxBodyAngle + 2) * maxAnimSpeed));
            }
            else
            {
                legAnim.SetFloat("speedMult", -(bodyRotation / (maxBodyAngle + 2) * maxAnimSpeed));
            }

            //Leg animation speed

            if (Input.GetButtonDown(jumpInput) && grounded)
            {
                rb.AddForce(Vector2.up * jumpPower);
            }

            fallingExecuted = false;
        }
        else if (!fallingExecuted)   // Only executes once everytime the waiter falls over
        {
            legAnim.SetFloat("speedMult", 1f);  // set anim speed to 1
            legAnim.SetBool("fallen", true);
            bodyAnim.SetBool("fallen", true);
            fallOver();
            fallingExecuted = true;
            GetComponent<ReferenceScript>().tray.SetActive(false);

            //*
            GameObject body = GetComponent<ReferenceScript>().body;
            body.GetComponent<BoxCollider2D>().enabled = false;
            body.GetComponent<CircleCollider2D>().enabled = true;
            //*/

            Invoke("standUp", 2.5f);
        }

    }

    /* 
     * Fall Over simulates the waiter tipping over and loosing control.
     * The method achieves it by removing control from the player and enabling rotation on the legs,
     * making it look like the waiter is falling over.
     */
    private void fallOver()
    {
        legRespawnPosition = transform.position;        // records the position where the player fell over

        mouseBalanceScript.setEnableRotation(false);    // removes rotation control
        rb.constraints = RigidbodyConstraints2D.None;   // lets the legs fall over like a ragdoll
        //bodyGameObject.GetComponent<BoxCollider2D>().isTrigger = false; // turns the bodies hitbox into non-trigger meaning its collision is on
    }

    /*  
     * Stand Up returns the waiter to the location where he lost control.
     * It also sets the legs and the body upright.
     */
    private void standUp()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);             // sets rotation to 0
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;     // locks rotation
        transform.position = legRespawnPosition;

        legAnim.SetBool("fallen", false);
        bodyAnim.SetBool("fallen", false);

        // Construct new body pos, so it's alligned with legs and has the correct distance from the legs
        Vector2 newBodyPosition = legRespawnPosition;
        newBodyPosition += initBodyPosTrans;

        bodyTransform.rotation = Quaternion.Euler(0, 0, 0);         // sets body's rotation to 0
        bodyTransform.position = newBodyPosition;

        standing = true;
        mouseBalanceScript.setEnableRotation(true);                    // Returns rotation control
        bodyGameObject.GetComponent<BoxCollider2D>().isTrigger = true;  // turns the bodies hitbox into a trigger meaning its collision is off
        GetComponent<ReferenceScript>().tray.SetActive(true);

        //*
        GameObject body = GetComponent<ReferenceScript>().body;
        body.GetComponent<BoxCollider2D>().enabled = true;
        body.GetComponent<CircleCollider2D>().enabled = false;
        //*/
    }

    /*
     * Purpose of applyMovement is to move the waiter left and right, 
     * depending on how much he is leaning over
     */
    private void applyMovement(float bodyRotation)
    {
        rb.AddForce(new Vector2(bodyRotation / (maxBodyAngle + 2) * tiltSpeed, 0f)); // + 2 for corrections

        Vector3 easeVelocity = rb.velocity;
        easeVelocity.y = rb.velocity.y;
        easeVelocity.z = 0.0f;
        easeVelocity.x *= 0.75f;

        rb.velocity = easeVelocity;

        // Limit speed
        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        }

        if (rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        }
    }

    public bool getStanding()
    {
        return standing;
    }
    public void setSpeed(float max, float tilt)
    {
        maxSpeed = maxSpeed * max;
        tiltSpeed = tiltSpeed * tilt;
        Invoke("halfSpeed", 2);
    }
    private void halfSpeed()
    {
        maxSpeed = maxSpeed / 3;
        tiltSpeed = tiltSpeed / 3;
    }
    public void setAngle()
    {
        float bodyRotation = bodyTransform.eulerAngles.z;   // Record the rotation of the body
        if (bodyRotation > 180) // Right side tilting of the body
        {
            bodyTransform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else                    // Left side tilting of the body
        {
            bodyTransform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }
}
