using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSwaying : MonoBehaviour
{

    public ItemSpawner itemSpawnerScript;
    public List<Vector3> initialItemPos;    // positions of items when they are first added to the tray
    public float maxItemAngle;              // angle at which the first item will fall
    public float minItemAngle;              // angle at which the last item will fall
    public float horizontalMoveDist;        // how much an item pushes over before it can fall
    public bool itemsFall;
    public float glueTime;

    private List<GameObject> itemList;
    private int currentItemCount;
    private float currentTrayAngle;

	// Use this for initialization
	void Start ()
    {
        itemList = itemSpawnerScript.itemsOnTray;
        currentItemCount = itemList.Count;
        recordAllItemPos();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (currentItemCount < itemList.Count)
        {
            recordItemPosFrom(currentItemCount);
            currentItemCount = itemList.Count;
        }
        else if (currentItemCount > itemList.Count)
        {
            removeItemPosFrom(currentItemCount);
            currentItemCount = itemList.Count;
        }

        currentTrayAngle = transform.rotation.eulerAngles.z;
        if (currentTrayAngle > 180)
        {
            currentTrayAngle = 0 - (360 - currentTrayAngle);
        }

        float moveProportion = currentTrayAngle / maxItemAngle;
        

        if (itemsFall)
        {
            for (int i = 0; i < currentItemCount; i++)  // Make items sway
            {
                itemList[i].transform.localPosition = initialItemPos[i] + (itemList[i].transform.right * -moveProportion * horizontalMoveDist * (i + 1)); // Push each item over
            }

            dropItems();
        }
        else    
        {
            for (int i = 0; i < currentItemCount; i++)
            {
                itemList[i].transform.localPosition = initialItemPos[i];    // Keep items alligned straight up
            }
        }
    }

    void dropItems()
    {
        float angleLimit = 0;
        for (int i = currentItemCount; i > 0; i--)  // Check if an item has gone past the falling angle
        {
            angleLimit = maxItemAngle - ((maxItemAngle - minItemAngle) * ((float)(i - 1) / (itemSpawnerScript.maxAmount - 1)));    // Angle at which each individual item falls

            if (angleLimit < Mathf.Abs(currentTrayAngle))  // Make item fall
            {
                itemList[i - 1].transform.SetParent(null, true);                // Sets parent to null. 2nd argument makes it retain its world position

                itemList[i - 1].GetComponent<Rigidbody2D>().simulated = true;   // Item now simulates its rigidbody physics
                itemList[i - 1].GetComponent<Rigidbody2D>().velocity = GameObject.FindGameObjectWithTag("Body").GetComponent<Rigidbody2D>().velocity;   // give the item bodie's velocity
                
                itemList[i - 1].GetComponent<ItemScript>().fallen = true;       // Marks the item as fallen
                itemSpawnerScript.currentAmount--;                              // Reduces current amount in ItemSpawner script by 1

                itemList.RemoveAt(itemList.Count - 1);                          // Removes the item from the reference list and frees the list up for other items to be picked up
            }
        }
    }

    private void removeItemPosFrom (int from)
    {
        for (; from > itemList.Count; from--)
        {
            initialItemPos.RemoveAt(initialItemPos.Count - 1);
        }
    }

    private void recordItemPosFrom (int from)
    {
        for (; from < itemList.Count; from++)
        {
            initialItemPos.Add(itemList[from].transform.localPosition);
        }
    }

    private void recordAllItemPos ()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            initialItemPos.Add(itemList[i].transform.localPosition);
        }
    }
    public void glueActivate()
    {
        itemsFall = false;
        Invoke("glueDeactivate", glueTime);
    }
    private void glueDeactivate()
    {
        itemsFall = true;
    }
}
