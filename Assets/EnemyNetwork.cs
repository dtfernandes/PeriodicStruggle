using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNetwork : MonoBehaviour
{
    public GameObject Target;
    public GameObject killedCanvas;
    public GameObject enemyCluster;
    public GameObject[] atomos;     //Átomos a fazer spawn pelo enemyCluster, 0=H  1=O  2=C
    public float speed;
    public int[] listofplayers;
    //tipo de inimigo; Se o tipo de inimigo for igual ao atomo do player entao ele segue o player

    public GameObject atomToSpawn;

    private bool active;

    public bool canWalk;
    public Vector2 direction;
    public bool canfollow;

    private Vector3 selfPos;

    public Sprite activeSprite, standBySprite;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (collision.gameObject.GetComponent<PhotonView>().isMine)
            {
                

                PhotonPlayer ola = PhotonNetwork.player;

                if (collision.gameObject.GetComponent<PhotonView>().isMine)
                {
                    GetComponent<PhotonView>().TransferOwnership(ola);
                    //killedCanvas.GetComponent<AudioSource>().Play();
                    switch (gameObject.name)
                    {
                        case "EnemyH(Clone)":
                            atomToSpawn = atomos[0];
                            break;
                        case "EnemyO(Clone)":
                            atomToSpawn = atomos[1];
                            break;
                        case "EnemyC(Clone)":
                            atomToSpawn = atomos[2];
                            break;
                    }

                    GameObject spawnable = PhotonNetwork.Instantiate(enemyCluster.name, transform.position, Quaternion.identity, 0);
                    spawnable.GetComponent<EnemyClusterNetworking>().atomosSpawn = atomToSpawn;
                    spawnable.GetComponent<EnemyClusterNetworking>().SpawnCluster();

                    GameObject sliders = null;

                    foreach (GameObject slide in GameObject.FindGameObjectsWithTag("scoreP"))
                    {
                        if (slide.GetComponent<PhotonView>().isMine)
                        {
                            sliders = slide;
                        }
                    }
                    if (sliders != null)
                    {
                        sliders.GetComponent<ScoreManagerNetwork>().score  += 1;
                    }
                    PhotonNetwork.Destroy(gameObject);
                    PhotonNetwork.Destroy(collision.gameObject);

                }


            }
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Mplayer")
        {
            if (canfollow)
            {
                //SceneManager.LoadScene("GameOverScene");
            }
        }

        if (collision.gameObject.name == "Border")
        {
            PhotonNetwork.Destroy(gameObject);
        }

    }

    void Start()
    {
        killedCanvas = GameObject.Find("Canvas");
    }

    void Update()
    {

        Target = GameObject.FindGameObjectWithTag("Player");
        



        if (GetComponent<PhotonView>().isMine)
        {
            EnemyMovement();
        }
        else
        {
            SmoothNetMovement();
        }

        if (transform.position.x > 26 || transform.position.x < -24 || transform.position.y > 22 || transform.position.y < -19)
        {

            PhotonNetwork.Destroy(gameObject);

        }


    }

    public void EnemyMovement()
    {

            canfollow = false;

            GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Mplayer");
            List<GameObject> targetsToFollow = new List<GameObject>(){ };

            foreach (GameObject currentTarget in allTargets) {

                for (int i = 0; i < listofplayers.Length; i++)
                {                   
                    if (currentTarget.GetComponent<PMultiplayerController>() != null)
                    {
                        if (listofplayers[i] == currentTarget.GetComponent<PMultiplayerController>().upgradeLevel)
                        {
                            if (!targetsToFollow.Contains(currentTarget))
                            {
                                targetsToFollow.Add(currentTarget);
                            }
                            canfollow = true;
                        }
      /*                  else
                        {
                             if (targetsToFollow.Count > 0)
                             {
                                 if (targetsToFollow.Contains(currentTarget))
                                 {
                                     targetsToFollow.Remove(currentTarget);
                                 }
                             }
                        }*/
                    }
                }



            }

            if (targetsToFollow.Count > 1)
            {

                float distanceTo0 = Mathf.Abs(Mathf.Sqrt(Mathf.Pow(transform.position.x - targetsToFollow[0].transform.position.x, 2) + Mathf.Pow(transform.position.y - targetsToFollow[0].transform.position.y, 2)));
                float distanceTo1 = Mathf.Abs(Mathf.Sqrt(Mathf.Pow(transform.position.x - targetsToFollow[1].transform.position.x, 2) + Mathf.Pow(transform.position.y - targetsToFollow[1].transform.position.y, 2)));

                if (distanceTo0 - distanceTo1 > 0)
                {
                    Target = targetsToFollow[1];
                }
                else { Target = targetsToFollow[0]; }

            }
            else if (targetsToFollow.Count == 1) { Target = targetsToFollow[0]; }

            if (canfollow)
            {
            
                GetComponent<SpriteRenderer>().sprite = activeSprite;
                transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, speed * Time.deltaTime);

            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = standBySprite;
                transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
                if (!canWalk)
                {

                    StartCoroutine("walkDirection");
                    canWalk = true;

                }

            }    

    }

    public void SmoothNetMovement()
    {
        transform.position = Vector3.Lerp(transform.position, selfPos, Time.deltaTime * 8);
        if (active)
        {
            GetComponent<SpriteRenderer>().sprite = activeSprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = standBySprite;
        }
    }

    IEnumerator walkDirection()
    {
        direction.x = Random.Range(-0.5f, 0.5f);
        direction.y = Random.Range(-0.5f, 0.5f);
        yield return new WaitForSeconds(Random.Range(1.1f, 2.2f));
        canWalk = false;
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(canfollow);
            stream.SendNext(canfollow);
        }
        else
        { 
            selfPos = (Vector3)stream.ReceiveNext();
            active = (bool)stream.ReceiveNext();
            canfollow = (bool)stream.ReceiveNext();
        }

    }

}
