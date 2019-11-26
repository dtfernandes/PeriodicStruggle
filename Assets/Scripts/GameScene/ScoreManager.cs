using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public int score;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {


        /*PlayerPrefs.SetInt("score", score);

        if(PlayerPrefs.GetInt("highscore") < score)
        {

            PlayerPrefs.SetInt("highscore", score);

        }*/

        GetComponent<Text>().text = "" + score;
        PlayerPrefs.SetInt("Score", score);

	}
}
