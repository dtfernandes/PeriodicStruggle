using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiveScoreAndName : MonoBehaviour
{
    int score1, score2, score3;
    string name1, name2, name3;

    int score;
    string name;

    void Start()
    {
        score = PlayerPrefs.GetInt("Score");
        score1 = PlayerPrefs.GetInt("Score1");
        score2 = PlayerPrefs.GetInt("Score2");
        score3 = PlayerPrefs.GetInt("Score3");        
        name1 = PlayerPrefs.GetString("Player1");
        name2 = PlayerPrefs.GetString("Player2");
        name3 = PlayerPrefs.GetString("Player3");
    }

    public void GiveValues()
    {
        name = PlayerPrefs.GetString("CurrentPlayerName");
        if (score1 < score)
        {
            //scores[2].text = scores[1].text;
            PlayerPrefs.SetInt("Score3", score2);
            //scores[1].text = scores[0].text;
            PlayerPrefs.SetInt("Score2", score1);
            //scores[0].text = "" + score;
            PlayerPrefs.SetInt("Score1", score);
            //names[2].text = names[1].text;
            PlayerPrefs.SetString("Player3", name2);
            //names[1].text = names[0].text;
            PlayerPrefs.SetString("Player2", name1);
            //names[0].text = name;
            PlayerPrefs.SetString("Player1", name);           
        }
        else if (score2 < score)
        {
            //scores[2].text = scores[1].text;
            PlayerPrefs.SetInt("Score3", score2);
            //scores[1].text = "" + score;
            PlayerPrefs.SetInt("Score2", score);
            //names[2].text = names[1].text;
            PlayerPrefs.SetString("Player3", name2);
            //names[1].text = name;
            PlayerPrefs.SetString("Player2", name);
        }
        else if (score3 < score)
        {
            //scores[2].text = "" + score;
            PlayerPrefs.SetInt("Score3", score);
            //names[2].text = name;
            PlayerPrefs.SetString("Player3", name);
        }
    }
}
