using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMultiplayerController : Photon.MonoBehaviour
{
    public bool devTesting = false;
    public PhotonView photonView;
    public float speed;
    public float fireRate;
    public int upgradeLevel;
    public int maxLives, currentLives;
    public GameObject electrons;

    public GameObject deathPanel;

    public bool dead;

    public Sprite[] sprites;

    public bool canfire;
    public Rigidbody2D myRigidbody;
    private Vector3 selfPos, selfRot;

    public GameObject plCamera, playerObject, plCanvas;

    private GameObject sceneCam;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemys")
        {
            if (GetComponent<PhotonView>().isMine)
            {
                if (collision.gameObject.GetComponent<EnemyNetwork>().canfollow)
                {
                    List<GameObject> electronsToDestroy = new List<GameObject> { };
                    foreach (GameObject ele in GameObject.FindGameObjectsWithTag("electrons"))
                    {
                        if (ele.GetComponent<PhotonView>().isMine)
                        {
                            if (!electronsToDestroy.Contains(ele))
                            {
                                electronsToDestroy.Add(ele);
                            }
                        }
                    }


                    if (electronsToDestroy.Count > 0)
                    {                   
                        PhotonNetwork.Destroy(electronsToDestroy[0]);
                    }
                    //(GameObject.FindGameObjectsWithTag("electrons")[0]);
                    currentLives -= 1;


                    collision.gameObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player);
                    PhotonNetwork.Destroy(collision.gameObject);

                    
                }
            }
        }

    }


    public void Death()
    {
        deathPanel.SetActive(true);
        upgradeLevel = 0;
    }

    private void Awake()
    {
        if (GetComponent<PhotonView>().isMine)
        {
            for (int i = 0; i < maxLives; i++)
            {
                if (GameObject.FindGameObjectsWithTag("electrons").Length < maxLives)
                {
                    PhotonNetwork.Instantiate(electrons.name, transform.position, Quaternion.identity,0);
                }
            }
            currentLives = maxLives;
        }

        if (!devTesting && photonView.isMine)
        {
            
            sceneCam = GameObject.Find("Main Camera");
            sceneCam.SetActive(false);
            plCamera.SetActive(true);

        }
    }

    private void Update()
    {

        

        if (!devTesting)
        {
            if (photonView.isMine && !dead)
            {
                checkInput();
               
            }
            else
            {
                smoothNetMovement();
                plCanvas.SetActive(false);
                plCamera.SetActive(false);
            }
        }
        else
        {
            checkInput();
        }
    }

    private void checkInput()
    {


        if(currentLives <= 0)
        {
            dead = true;
            Death();
        }
            
        foreach (Transform child in transform)
        {
           if(child.gameObject.tag == "Player")
           {
                playerObject = child.gameObject;
           }

        }

      
        myRigidbody.velocity = new Vector2(0f, 0f);

        if (Input.GetAxisRaw("Horizontal") > 0.5 || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, myRigidbody.velocity.y);
        }

        if (Input.GetAxisRaw("Vertical") > 0.5 || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, Input.GetAxisRaw("Vertical") * speed);
        }


        //Player Rotation
        if (playerObject != null)
        {
            float AngleRad = Mathf.Atan2(plCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition).x - playerObject.transform.position.x, plCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition).y - playerObject.transform.position.y);
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            playerObject.transform.rotation = Quaternion.Euler(0, 0, -AngleDeg + 90);
        }

        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            if (canfire)
            {
                
                foreach (GameObject gun in GameObject.FindGameObjectsWithTag("Gun"))
                {

                    if (gun.transform.parent == playerObject.transform)
                    {
                        gun.GetComponent<GunScriptNetwork>().Player = gameObject;
                        StartCoroutine(gun.GetComponent<GunScriptNetwork>().FireBullet());
                    }

                }
                canfire = false;
            }
        }


    }

    private void smoothNetMovement()
    {

        transform.position = Vector3.Lerp(transform.position, selfPos, Time.deltaTime * 8);
        
        foreach (GameObject pl in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (pl.transform.parent != null)
            {
                if (!pl.transform.parent.GetComponent<PhotonView>().isMine)
                {
                    pl.GetComponent<SpriteRenderer>().sprite = sprites[upgradeLevel];
                    pl.transform.eulerAngles = Vector3.Lerp(pl.transform.eulerAngles, new Vector3(pl.transform.eulerAngles.x, pl.transform.eulerAngles.y, selfRot.z), Time.deltaTime * 8);
                }
            }
        }

    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.isWriting)
        {
            stream.SendNext(transform.position);

            bool o = false;
            foreach (GameObject pl in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (pl.transform.parent != null)
                {
                    if (pl.transform.parent.GetComponent<PhotonView>().isMine)
                    {
                        stream.SendNext(pl.transform.eulerAngles);
                        o = true;
                    }
                   
                }
               
            }
            
            if(o == false)
            {
                stream.SendNext(Vector3.zero);
            }

            stream.SendNext(upgradeLevel);
            
        }
        else
        {
            
            selfPos = (Vector3)stream.ReceiveNext();
            selfRot = (Vector3)stream.ReceiveNext();
            upgradeLevel = (int)stream.ReceiveNext();

        }

    }

}
