using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

    public GameObject gameClock;
    public GameObject player1;
    public GameObject player2;
    public Text endGameText;
    float timeBeforeReset = 5.0f;

    // Update is called once per frame
    void Update () {
		if(gameClock.GetComponent<TimerScript>().done == true)
        {
            //Time.fixedDeltaTime = 0f;
            if (player1.GetComponent<ScoreForPlayer>().score > player2.GetComponent<ScoreForPlayer>().score)
            {
                endGameText.text = "Player 1 WINS!!";
            }
            else if(player1.GetComponent<ScoreForPlayer>().score < player2.GetComponent<ScoreForPlayer>().score)
            {
                endGameText.text = "Player 2 WINS!!";
            }
            else
            {
                endGameText.text = "DRAW!!";
            }

            timeBeforeReset -= Time.deltaTime;
            if (timeBeforeReset <= 0)
            {
                SceneManager.LoadScene(0);
            }
        }
	}
}
