using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUnrestricted : MonoBehaviour {

    public GameObject Target;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Target = GameObject.FindGameObjectWithTag("Player");

        transform.position = Target.transform.position;

	}
}
