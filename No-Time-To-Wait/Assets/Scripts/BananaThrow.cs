using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaThrow : MonoBehaviour
{
     
    public float floatMovementHeight;   // max distance a power up can move in a single frame
    public int floatCycles;             // higher number = slower floating
    public GameObject banana;
    public float throwForce;
    public int playerNum;
    public GameObject legs;
    public GameObject body;
    public GameObject tray;
    public string throwInput = "Throw_P1";

    private int currentCycle;

    // Use this for initialization
    void Start()
    {
        currentCycle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown(throwInput))
        {
            Debug.Log("Thrown");
            //Vector3 originalPos = transform.position;
            GameObject bananaInstance = Instantiate(banana, null, false);
            bananaInstance.transform.position = tray.transform.position;

            if (playerNum == 2)
            {
                throwForce = -(Mathf.Abs(throwForce));
            }

            bananaInstance.AddComponent<Rigidbody2D>();
            bananaInstance.GetComponent<Rigidbody2D>().AddForce(body.transform.right * throwForce);

            Destroy(gameObject);
        }
    }
}
