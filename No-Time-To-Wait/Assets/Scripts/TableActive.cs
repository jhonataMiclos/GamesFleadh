using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableActive : MonoBehaviour {
    
    public float tableWantTime;   //creates a random time for table to fill up
    public bool active = false;   //boolean to see if table is active
    public bool playerServed = false;
    public int numberAtTable = 0;
    public int tableType = 0;     //0 = normal, 1 = blue, 2 = red
    public Sprite whiteTable;
    public Sprite blueTable;
    public Sprite redTable;


    // Use this for initialization
    void Start () {
        tableWantTime = Random.Range(1, 10);
        if (tableType == 1)
        {
            this.GetComponent<SpriteRenderer>().sprite = blueTable;
        }
        else if (tableType == 2)
        {
            this.GetComponent<SpriteRenderer>().sprite = redTable;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = whiteTable;
        }
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().text = numberAtTable.ToString();
        if (gameObject.GetComponentInChildren<Canvas>().GetComponent<TimerScript>().done && numberAtTable <= 0)
        {
            numberAtTable = Random.Range(1, 5);
            active = true;
        }
        else
            playerServed = false;

        if (playerServed)
        {
            gameObject.GetComponentInChildren<Canvas>().GetComponent<TimerScript>().done = false;
            active = false;
        }

        if (!gameObject.GetComponentInChildren<Canvas>().GetComponent<TimerScript>().done)
        {
            numberAtTable = 0;
        }
        if (numberAtTable < 0)
            numberAtTable = 0;
        
    }
}
