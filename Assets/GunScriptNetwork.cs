using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScriptNetwork : MonoBehaviour
{
    public GameObject bullets;
    public GameObject bullet, gunend;
    public GameObject Player;

    public Vector3 finalposition;
    public Vector3 startPos;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator FireBullet()
    {
        finalposition = gunend.transform.position;
        startPos = transform.position;
        bullet = PhotonNetwork.Instantiate(bullets.name, transform.position, Quaternion.identity,0);
        bullet.GetComponent<BulletNetwork>().startPos = startPos;
        bullet.GetComponent<BulletNetwork>().mousePos = finalposition;
        yield return new WaitForSeconds(Player.GetComponent<PMultiplayerController>().fireRate);
        if (Player.GetComponent<PMultiplayerController>() != null)
        {
            Player.GetComponent<PMultiplayerController>().canfire = true;
        }
    }
}
