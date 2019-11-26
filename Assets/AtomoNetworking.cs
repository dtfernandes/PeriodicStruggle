using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AtomoNetworking : MonoBehaviour
{
    public GameObject sliders;
    public GameObject[] player;
    public GameObject pickUpSound;
    public Vector3 selfPos;

    void Start()
    {
        pickUpSound = GameObject.FindWithTag("Sound");
        
        
        foreach (GameObject slide in GameObject.FindGameObjectsWithTag("Sliders"))
        {
            if (slide.transform.parent.transform.parent.GetComponent<PhotonView>().isMine)
            {
                sliders = slide;
            }
        }

    }

    void Update()
    {
       

        if (!GetComponent<PhotonView>().isMine)
        {
            smoothNetMovement();
        }

        player = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject p in player)
        {
            foreach (Collider2D coll in p.GetComponents<Collider2D>())
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), coll);
            }
        }


    }

    private void smoothNetMovement()
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (sliders != null)
            {
                if (sliders.GetComponent<SliderManager>() != null)
                {
                    switch (gameObject.name)
                    {
                        case "Hidrogenio(Clone)":
                            sliders.GetComponent<SliderManager>().values[0] += 1;
                            break;
                        case "Oxigenio(Clone)":
                            sliders.GetComponent<SliderManager>().values[1] += 1;
                            break;
                        case "Carbono(Clone)":
                            sliders.GetComponent<SliderManager>().values[2] += 1;
                            break;
                    }
                }
                if (sliders.GetComponent<SliderManagerMultipayer>() != null)
                {
                    switch (gameObject.name)
                    {
                        case "Hidrogenio(Clone)":
                            sliders.GetComponent<SliderManagerMultipayer>().values[0] += 1;
                            break;
                        case "Oxigenio(Clone)":
                            sliders.GetComponent<SliderManagerMultipayer>().values[1] += 1;
                            break;
                        case "Carbono(Clone)":
                            sliders.GetComponent<SliderManagerMultipayer>().values[2] += 1;
                            break;
                    }
                }
            }

            pickUpSound.GetComponent<AudioSource>().Play();

            PhotonPlayer ola = PhotonNetwork.player;

            if (collision.gameObject.transform.parent.GetComponent<PhotonView>().isMine)
            {
                GetComponent<PhotonView>().TransferOwnership(ola);
                PhotonNetwork.Destroy(gameObject);
            }

        }



    }


}


