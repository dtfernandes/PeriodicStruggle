using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGunScript : MonoBehaviour {

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
        yield return new WaitForSeconds(GameObject.FindGameObjectWithTag("Mplayer").GetComponent<PMultiplayerController>().fireRate);      
        if (GameObject.FindGameObjectWithTag("Mplayer").GetComponent<PMultiplayerController>() != null)
        {

            GameObject.FindGameObjectWithTag("Mplayer").GetComponent<PMultiplayerController>().canfire = true;

        }
    }

}
