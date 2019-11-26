using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHScript : MonoBehaviour {

    public float value;
    public Text hText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

     

        value = GetComponent<Slider>().value;

        hText.text = "" + value;


    }
}
