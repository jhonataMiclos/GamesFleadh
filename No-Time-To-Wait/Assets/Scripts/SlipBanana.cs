using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipBanana : MonoBehaviour
{


	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Legs"
            && collision.gameObject.GetComponent<PlayerMovement>().getStanding())   // if the player touches the banana and is standing
        {
            collision.gameObject.GetComponent<PlayerMovement>().standing = false;
           Destroy(gameObject); // destroys the game object, making it disappear
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit a trigger");
        if (other.tag == "Body"
            && other.gameObject.GetComponent<ReferencesOnBody>().legs.GetComponent<PlayerMovement>().getStanding())   // if the player touches the banana and is standing
        {
            other.gameObject.GetComponent<ReferencesOnBody>().legs.GetComponent<PlayerMovement>().standing = false;
           Destroy(gameObject); // destroys the game object, making it disappear
        }
    }
}
