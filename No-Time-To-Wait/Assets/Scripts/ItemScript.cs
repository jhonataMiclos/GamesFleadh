using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {

    public Transform itemStack;
    public bool fallen;
    public float destroyTime = 5f;

    private float destroyTimer;
    private Rigidbody2D rb;

	void Start () {
        destroyTimer = destroyTime;
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!fallen)
        {
            this.transform.rotation = itemStack.transform.rotation;         //Updates items rotation to be same as stack
            destroyTimer = destroyTime;
        }
        else
        {
            destroyTimer -= Time.deltaTime;
            if (destroyTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
        
	}

    public void setTrayAsParent (Transform itemStack) {
        this.itemStack = itemStack;
        this.transform.rotation = itemStack.transform.rotation;         //Sets items roatation to same as stack
        this.transform.SetParent(itemStack.transform, true);            //Makes item child of stack
        fallen = false;                                                 //True if the item has fallen from the tray
    }
}
