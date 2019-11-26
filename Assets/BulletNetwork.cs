using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNetwork : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 mousePos;
    public Vector3 selfPos;
    public float speed;

    // Use this for initialization
    void Start()
    {

        StartCoroutine(DestroyObject());

    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3((mousePos.x - startPos.x), (mousePos.y - startPos.y), 0).normalized * speed * Time.deltaTime;


        if (GetComponent<PhotonView>().isMine)
        {
            transform.position += new Vector3((mousePos.x - startPos.x), (mousePos.y - startPos.y), 0).normalized * speed * Time.deltaTime;
        }
        else
        {
            SmoothNetMovement();
        }
    }

    public void SmoothNetMovement()
    {
        transform.position = Vector3.Lerp(transform.position, selfPos, Time.deltaTime * 8);
       
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            selfPos = (Vector3)stream.ReceiveNext();
        }

    }
}
