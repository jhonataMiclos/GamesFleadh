using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenSpawn : MonoBehaviour {

    //public float kitchenSpawnTime = 2.0f;       //Time for food to be cooked
    public bool foodReady = true;               //Is the food ready?
    public List<GameObject> itemsForCollection = new List<GameObject>();
    public int maxAmount;
    public int currentAmount;
    public float foodDist;
    public bool removeAFood = false;
    public Transform spawnPos;
    public int kitchenNum = 1;

    // Use this for initialization
    void Start () {
        //itemStack = this.GetComponent<GameObject>();

    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.GetComponentInChildren<Canvas>().GetComponent<TimerScript>().done && currentAmount != maxAmount)
        {
            foodReady = true;
            Object[] subListObjects = Resources.LoadAll("Items", typeof(GameObject));
            //Debug.Log(subListObjects[0].name);

            foreach (GameObject subListObject in subListObjects)
            {
                //Debug.Log("AddingToList");
                GameObject temp = (GameObject)subListObject;
                if (temp.transform.tag == "FoodItem")
                {
                    itemsForCollection.Add(Instantiate(temp, spawnPos.position + (transform.up * foodDist * (itemsForCollection.Count + 1)), Quaternion.identity));   // spawn food unit on the tray with appropriate foodDist
                    Transform itemStack = transform.Find("ItemStack");  // finds the transform of ItemStack of the appropriate player
                    itemsForCollection[itemsForCollection.Count - 1].GetComponent<ItemScript>().setTrayAsParent(itemStack);    // Set the tray (of the appropriate player) as the parent of the item
                    currentAmount++;
                }
            }
            gameObject.GetComponentInChildren<Canvas>().GetComponent<TimerScript>().done = false;
        }
        (gameObject.GetComponent(typeof(Collider2D)) as Collider2D).enabled = foodReady;
        if (removeAFood)
        {
            Destroy(itemsForCollection[itemsForCollection.Count - 1], 0.2f);
            itemsForCollection.Remove(itemsForCollection[itemsForCollection.Count - 1]);
            currentAmount--;
            removeAFood = false;
        }
    }


}