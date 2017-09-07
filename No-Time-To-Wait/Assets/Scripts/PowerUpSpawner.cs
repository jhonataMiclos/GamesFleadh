using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{

    // Use this for initialization
    public GameObject speed;
    public GameObject glue;
    public GameObject banana;
    public float timeOfCooldown ;
    public bool cooldown = true;
    public float maxNumOfPower;
    public float counter;
    private float originalTime;
    void Start()
    {
        originalTime = timeOfCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(counter < 0)
        {
            counter = 0;
        }
        if(counter < 1)
        {
            if (timeOfCooldown > 0)
            {
                timeOfCooldown -= Time.deltaTime;
            }
            else
            {
                spawner();
                timeOfCooldown = originalTime;
            }
        }
       
       /* timeOfCooldown -= Time.deltaTime;                         //Counts down time

            if (timeOfCooldown == 0.0f || timeOfCooldown <= 0.0f)   //If it reaches 0
            {
                cooldown = true;
                spawner();
            }
        else
            timeOfCooldown = 0.1f;*/
        //gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().text = timeOfCooldown.ToString("F1");
        // (gameObject.GetComponent(typeof(Collider2D)) as Collider2D).enabled = cooldown;
    }
    public void spawner()
    {
        Debug.Log("out side if");

        if ( counter < maxNumOfPower)
        {

            GameObject powerUpToSpawn;
            Debug.Log("hit the kitchen");
            if (Random.value >= 0)
            {
                Debug.Log("possible to spawn");

                if (Random.value < 0.33 && Random.value > 0)
                {
                    Debug.Log("try to spawn");
                    powerUpToSpawn = Instantiate(speed, gameObject.transform.position, Quaternion.identity);
                    powerUpToSpawn.transform.SetParent(gameObject.transform);
                   // powerUpToSpawn.AddComponent<Rigidbody2D>();
                   timeOfCooldown = 10.0f;
                    counter += 1;

                    //  powerUpToSpawn = speed;
                }
                else if (Random.value < 0.66 && Random.value > 0.33)
                {
                    Debug.Log("try to spawn");
                    powerUpToSpawn = Instantiate(glue, gameObject.transform.position, Quaternion.identity);
                    powerUpToSpawn.transform.SetParent(gameObject.transform);
                    // powerUpToSpawn.AddComponent<Rigidbody2D>();
                    timeOfCooldown = 10.0f;
                    counter += 1;

                    // powerUpToSpawn = glue;
                }
                else if (Random.value < 0.66 && Random.value > 0.33)
                {
                    Debug.Log("try to spawn");
                    powerUpToSpawn = Instantiate(banana, gameObject.transform.position, Quaternion.identity);
                     powerUpToSpawn.transform.SetParent(gameObject.transform);
                    // powerUpToSpawn.AddComponent<Rigidbody2D>();
                    timeOfCooldown = 10.0f;
                    counter += 1;

                    // powerUpToSpawn = banana;
                }
            }
        }
    }
    /*void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("out side if");

        if (col.gameObject.tag == "Legs" && counter < maxNumOfPower)
        {

            GameObject powerUpToSpawn;
            Debug.Log("hit the kitchen");
            if (Random.value >= 0)
            {
                Debug.Log("possible to spawn");
                if (Random.value < 0.33 && Random.value > 0)
                {
                    Debug.Log("try to spawn");
                    powerUpToSpawn = Instantiate(speed, new Vector3(11.0f, -37.0f, 0f), Quaternion.identity);
                    // powerUpToSpawn.AddComponent<Rigidbody2D>();
                    timeOfCooldown = 10.0f;
                    counter += 1;

                    //  powerUpToSpawn = speed;
                }
                else if (Random.value < 0.66 && Random.value > 0.33)
                {
                    Debug.Log("try to spawn");
                    powerUpToSpawn = Instantiate(glue, new Vector3(11.0f, -37.0f, 0f), Quaternion.identity);
                    // powerUpToSpawn.AddComponent<Rigidbody2D>();
                    timeOfCooldown = 10.0f;
                    counter += 1;

                    // powerUpToSpawn = glue;
                }
                else if (Random.value < 0.66 && Random.value > 0.33)
                {
                    Debug.Log("try to spawn");
                    powerUpToSpawn = Instantiate(banana, new Vector3(11.0f, -37.0f, 0f), Quaternion.identity);
                    // powerUpToSpawn.AddComponent<Rigidbody2D>();
                    timeOfCooldown = 10.0f;
                    counter += 1;

                    // powerUpToSpawn = banana;
                }
                else if (Random.value < 0.66 && Random.value > 0.33)
                {

                    /* Debug.Log("try to spawn");
                     powerUpToSpawn = Instantiate(poision, new Vector3(11.0f, -37.0f, 0f), Quaternion.identity);
                     // powerUpToSpawn.AddComponent<Rigidbody2D>();
                     timeOfCooldown = 10.0f;
                     counter += 1;
                }



            }
        }

    }*/
}

