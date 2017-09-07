using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapScript : MonoBehaviour {

    public GameObject table1;
    public GameObject table2;
    public GameObject table3;
    public GameObject table4;
    public GameObject table5;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Find("Table1").GetComponent<Text>().text = table1.GetComponent<TableActive>().numberAtTable.ToString();
        transform.Find("Table2").GetComponent<Text>().text = table2.GetComponent<TableActive>().numberAtTable.ToString();
        transform.Find("Table3").GetComponent<Text>().text = table3.GetComponent<TableActive>().numberAtTable.ToString();
        transform.Find("Table4").GetComponent<Text>().text = table4.GetComponent<TableActive>().numberAtTable.ToString();
        transform.Find("Table5").GetComponent<Text>().text = table5.GetComponent<TableActive>().numberAtTable.ToString();
    }
}
