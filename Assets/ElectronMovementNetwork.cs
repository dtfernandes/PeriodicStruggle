using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronMovementNetwork : MonoBehaviour
{
    public GameObject end, beginning;
    public float speed;

    public Sprite[] electronColors;
    public Vector3 selfPos;

    public int direction, electronType;

    // Use this for initialization
    void Start()
    {
        if (GetComponent<PhotonView>().isMine)
        {
            electronType = Random.Range(0, electronColors.Length);
            GetComponent<SpriteRenderer>().sprite = electronColors[electronType];
            direction = Random.Range(0, 2);
        }


    }

    // Update is called once per frame
    void Update()
    {

        if (GetComponent<PhotonView>().isMine)
        {
            if (direction == 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, end.transform.position, speed * Time.deltaTime);
                GetComponent<SpriteRenderer>().sortingOrder = 2;
                if (transform.position == end.transform.position)
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
        else
        {
            smoothNetMovement();
        }
    }



    private void smoothNetMovement()
    {

        transform.position = Vector3.Lerp(transform.position, selfPos, Time.deltaTime * 8);
        GetComponent<SpriteRenderer>().sprite = electronColors[electronType];

    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(electronType);
        }
        else
        {

            selfPos = (Vector3)stream.ReceiveNext();
            electronType = (int)stream.ReceiveNext();

        }

    }

}
