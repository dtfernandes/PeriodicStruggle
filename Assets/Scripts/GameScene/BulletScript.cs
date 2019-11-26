using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public Vector3 startPos;
    public Vector3 mousePos;
    public Vector3 selfPos;
    public float speed;

	// Use this for initialization
	void Start () {

        StartCoroutine(DestroyObject());

	}
	
    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

	// Update is called once per frame
	void Update ()
    {
    
        transform.position += new Vector3((mousePos.x - startPos.x), (mousePos.y - startPos.y), 0).normalized * speed * Time.deltaTime;
     
	}

}
