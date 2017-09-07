using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {

    public List<GameObject> itemsOnTray = new List<GameObject>();
    public GameObject item; //Item that will be on tray
    public GameObject trayPos;
    public ScoreForPlayer parent; //Used to change score in parent
    public int maxAmount;
    public float foodDist;  // Distance between food pieces (from the center of 1 food unit to the center of the next food unit)
    public int currentAmount;
    public GameObject dollarBill;
    public string inputString = "Item_P1";
    public int playerNum;

    private float time = 0.2f;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if (Input.GetButtonDown(inputString))
        {
            //When hit kitchen take food
            if (col.gameObject.tag == "Kitchen" && currentAmount < maxAmount && playerNum == col.gameObject.GetComponent<KitchenSpawn>().kitchenNum)
            {
                //Debug.Log("Collision");
                Object[] subListObjects = Resources.LoadAll("Items", typeof(GameObject));
                //Debug.Log(subListObjects[0].name);

                foreach (GameObject subListObject in subListObjects)
                {
                    //Debug.Log("AddingToList");
                    GameObject temp = (GameObject)subListObject;
                    if (temp.transform.tag == "FoodItem")
                    {
                        itemsOnTray.Add(Instantiate(item, transform.position + (transform.up * foodDist * (itemsOnTray.Count + 1)), Quaternion.identity));   // spawn food unit on the tray with appropriate foodDist
                        Transform itemStack = transform.Find("ItemStack");  // finds the transform of ItemStack of the appropriate player
                        itemsOnTray[itemsOnTray.Count - 1].GetComponent<ItemScript>().setTrayAsParent(itemStack);    // Set the tray (of the appropriate player) as the parent of the item
                        currentAmount++;
                        col.GetComponent<KitchenSpawn>().removeAFood = true;
                    }
                }
                col.gameObject.GetComponent<KitchenSpawn>().foodReady = false;
            }

            //When hit table remove food
            if (col.gameObject.tag == "Table" && col.gameObject.GetComponent<TableActive>().active && currentAmount >= 1 && (playerNum == col.gameObject.GetComponent<TableActive>().tableType || col.gameObject.GetComponent<TableActive>().tableType == 0))
            {
                Destroy(itemsOnTray[itemsOnTray.Count - 1], time);
                itemsOnTray.Remove(itemsOnTray[itemsOnTray.Count - 1]);
                currentAmount--;
                /*
                for (int i = col.gameObject.GetComponent<TableActive>().numberAtTable; i > 0; i--)
                {
                    Destroy(itemsOnTray[itemsOnTray.Count - 1], time);
                    itemsOnTray.Remove(itemsOnTray[itemsOnTray.Count - 1]);
                    currentAmount--;
                }
                */

                GameObject dollarInstance = Instantiate(dollarBill);
                dollarInstance.transform.position = transform.position;
                parent.score += 10;
                if (col.gameObject.GetComponent<TableActive>().numberAtTable == 1)
                {
                    parent.score += 10;
                    col.gameObject.GetComponent<TableActive>().playerServed = true;
                }
                col.gameObject.GetComponent<TableActive>().numberAtTable --;
            }
        }
    }
}
