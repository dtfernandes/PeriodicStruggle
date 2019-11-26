using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBetween : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start ()
    {

        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));

	}
	
	// Update is called once per frame
	void Update () {

        transform.eulerAngles += new Vector3(0,0,1) * speed * Time.deltaTime;

	}
}
