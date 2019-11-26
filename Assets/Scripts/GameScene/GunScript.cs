using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {

    public GameObject bullets;
    public GameObject bullet, gunend;

    
    public Vector3 finalposition;
    public Vector3 startPos;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator FireBullet()
    {
        finalposition = gunend.transform.position;
        startPos = transform.position;
        bullet = Instantiate(bullets, transform.position, Quaternion.identity);
        bullet.GetComponent<BulletScript>().startPos = startPos;
        bullet.GetComponent<BulletScript>().mousePos = finalposition;
        yield return new WaitForSeconds(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().fireRate);
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>() != null)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().canfire = true;
        }
    }

}
