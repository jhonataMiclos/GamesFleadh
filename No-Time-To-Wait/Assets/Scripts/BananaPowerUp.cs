﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaPowerUp : MonoBehaviour
{

    public GameObject banana;
    public float throwForce;
    public int playerNum;
    public GameObject legs;
    public GameObject body;
    public GameObject tray;
    public string throwInput = "Throw_P1";
    public float floatMovementHeight;   // max distance a power up can move in a single frame
    public int floatCycles;             // higher number = slower floating
    public GameObject ban;
    private int currentCycle;
    public bool picked = false;
    public GameObject trayPos;

	// Use this for initialization
	void Start ()
    {
        currentCycle = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!picked)
        {
            Vector2 updatedPos = transform.position;    // gets the current position of the power up
            updatedPos.y = updatedPos.y + (floatMovementHeight * Mathf.Cos(Mathf.PI * 2.0f * (1.0f * currentCycle / floatCycles))); // adds or takes away a small amount of distance. This gives an illusion of it floating up and down. Sin function makes the movement smooth

            transform.position = updatedPos;            // changes the position of the power up to this newly calculated position

            currentCycle++;

            if (currentCycle == floatCycles)            // resets the cycle once it reaches the max cycle (floatCycles)
            {
                currentCycle = 0;
            }
        }
	}
     void FixedUpdate()
     {
          if (Input.GetButtonDown(throwInput) && picked )
         {
             Debug.Log("Thrown");
              //Vector3 originalPos = transform.position;
             GameObject bananaInstance = Instantiate(banana, null, false);
             bananaInstance.transform.position = trayPos.transform.position;
             bananaInstance.transform.localScale -= new Vector3(0.2F, 0.2F, 0);

             if (playerNum == 2 )
             {
                throwForce = -(Mathf.Abs(throwForce));
               
            }
            bananaInstance.AddComponent<Rigidbody2D>();
            bananaInstance.GetComponent<Rigidbody2D>().AddRelativeForce(body.transform.right * throwForce);
            legs.GetComponent<PlayerMovement>().powerUpNum = 0;
            Destroy(gameObject);
         }
        if (picked)
        {
            if (!legs.GetComponent<PlayerMovement>().getStanding())
            {
                legs.GetComponent<PlayerMovement>().powerUpNum = 0;
                Destroy(gameObject);
            }
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        bool pickUp = false;
        int bodyPart = 0;   // 1 if body, 2 if legs
            if (collision.gameObject.tag == "Body" && collision.gameObject.GetComponent<ReferencesOnBody>().legs.GetComponent<PlayerMovement>().getStanding() && collision.gameObject.GetComponent<ReferencesOnBody>().legs.GetComponent<PlayerMovement>().powerUpNum == 0)
            {
                pickUp = true;
                bodyPart = 1;
            }
            else if (collision.gameObject.tag == "Legs" && collision.gameObject.GetComponent<PlayerMovement>().getStanding() && collision.gameObject.GetComponent<PlayerMovement>().powerUpNum == 0)
            {
                pickUp = true;
                bodyPart = 2;
            }
            if (pickUp && !picked)   // if the player touches the power up and is standing
            {
                this.GetComponentInParent<PowerUpSpawner>().counter--;
                if (bodyPart == 1)
                {
                    transform.SetParent(collision.gameObject.transform);
                    tray = collision.gameObject.GetComponent<ReferencesOnBody>().tray;
                    body = collision.gameObject;
                    trayPos = collision.gameObject.GetComponent<ReferencesOnBody>().legs.GetComponent<ReferenceScript>().trayPos;
                    playerNum = collision.gameObject.GetComponent<ReferencesOnBody>().legs.GetComponent<ReferenceScript>().playerNum;
                    this.transform.position = body.transform.position;
                    legs = collision.gameObject.GetComponent<ReferencesOnBody>().legs;
                }
                else if (bodyPart == 2)
                {
                    Debug.Log("PickUp");
                    tray = collision.gameObject.GetComponent<ReferenceScript>().tray;
                    body = collision.gameObject.GetComponent<ReferenceScript>().body;
                    trayPos = collision.gameObject.GetComponent<ReferenceScript>().trayPos;
                    transform.SetParent(body.transform, true);

                    playerNum = collision.gameObject.GetComponent<ReferenceScript>().playerNum;
                    this.transform.position = body.transform.position;
                    legs = collision.gameObject;
                }
                transform.localScale -= new Vector3(0.7F, 0.7F, 0);
                legs.GetComponent<PlayerMovement>().powerUpNum = 1;
                picked = true;
                if (legs.GetComponent<ReferenceScript>().playerNum == 1)
                {
                    throwInput = "Throw_P1";
                }
                else if (legs.GetComponent<ReferenceScript>().playerNum == 2)
                    throwInput = "Throw_P2";
            }
        
      
    }

 
}
