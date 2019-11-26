using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    public GameObject Target;

    public Vector2 pos;

	// Use this for initialization
	void Start () {
		
        

	}
	
	// Update is called once per frame
	void Update ()
    {

        Target = GameObject.FindGameObjectWithTag("Player");

        if (Target != null)
        {
            if(Target.transform.position.x > -14.9 && Target.transform.position.x < 16.9)
            {

                pos.x = Target.transform.position.x;

            }
          

            if (Target.transform.position.y > -14.2 && Target.transform.position.y < 16.6)
            {

                pos.y = Target.transform.position.y;

            }
           

            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
	}
}
