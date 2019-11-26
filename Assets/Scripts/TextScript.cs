using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextScript : MonoBehaviour {

    public string pref;
	
	void Update ()
    {
        GetComponent<Text>().text = "" + PlayerPrefs.GetInt(pref);
        if (gameObject.name == "Text")
        {
            GetComponent<Text>().text = "Score: " + PlayerPrefs.GetInt(pref);
        }
	}
}
