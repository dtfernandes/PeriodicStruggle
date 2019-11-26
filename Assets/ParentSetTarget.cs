using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentSetTarget : MonoBehaviour {

    private GameObject[] Target;
    private GameObject tTarget;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {

        Target = GameObject.FindGameObjectsWithTag("Mplayer");
        foreach(GameObject t in Target)
        {
            if (t.GetComponent<PhotonView>().isMine)
            {
                 tTarget = t;
            }
        }
        if (tTarget != null)
        {
            gameObject.transform.SetParent(tTarget.transform);
        }
	}
}
