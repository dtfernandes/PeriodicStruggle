using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronMovement : MonoBehaviour {

    public GameObject end, beginning;
    public float speed;

    public Sprite[] electronColors;

    public int direction;

    // Use this for initialization
    void Start ()
    {

        GetComponent<SpriteRenderer>().sprite = electronColors[Random.Range(0, electronColors.Length)];
        direction = Random.Range(0,2);


	}
	
	// Update is called once per frame
	void Update () {

        if (direction == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position,end.transform.position, speed * Time.deltaTime);
            GetComponent<SpriteRenderer>().sortingOrder = 2;
            if(transform.position == end.transform.position)
            {
                direction = 1;
            }
        }

        if (direction == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, beginning.transform.position, speed * Time.deltaTime);
            GetComponent<SpriteRenderer>().sortingOrder = 0;
            if (transform.position == beginning.transform.position)
            {
                direction = 0;
            }

        }


	}
}
