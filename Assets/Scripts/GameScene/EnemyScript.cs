using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour {

    public GameObject Target;
    public GameObject killedCanvas;
    public GameObject enemyCluster;
    public GameObject[] atomos;     //Átomos a fazer spawn pelo enemyCluster, 0=H  1=O  2=C 3=N
    public float speed;
    public int[] listofplayers;
    //tipo de inimigo; Se o tipo de inimigo for igual ao atomo do player entao ele segue o player; atribuido no inspector; int = upgradeLevel a seguir

    public bool canWalk;
    public Vector2 direction;
    public bool canfollow;

    public Sprite activeSprite, standBySprite;

    public GameObject score;


    void OnTriggerEnter2D(Collider2D collision)
     {
        if(collision.gameObject.tag == "Bullet")
        {
            //killedCanvas.GetComponent<AudioSource>().Play();
            switch (gameObject.name)
            {
                case "EnemyH(Clone)":
                    enemyCluster.GetComponent<EnemyCluster>().atomosSpawn = atomos[0];
                    break;
                case "EnemyO(Clone)":
                    enemyCluster.GetComponent<EnemyCluster>().atomosSpawn = atomos[1];
                    break;
                case "EnemyC(Clone)":
                    enemyCluster.GetComponent<EnemyCluster>().atomosSpawn = atomos[2];
                    break;
                case "EnemyN(Clone)":
                    enemyCluster.GetComponent<EnemyCluster>().atomosSpawn = atomos[3];
                    break;
            }               
            Instantiate(enemyCluster, transform.position, Quaternion.identity);
            GameObject.Find("Score").GetComponent<ScoreManager>().score += 5;
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }

     }

     void OnCollisionEnter2D(Collision2D collision)
     {
        if(collision.gameObject.tag == "Mplayer")
        {
            if (canfollow)
            {
             //   SceneManager.LoadScene("GameOverScene");
            }
        } 


     }

    void Start ()
    {
        score = GameObject.FindGameObjectWithTag("Score");
        killedCanvas = GameObject.Find("Canvas");
	}
	
	void Update () {

        Target = GameObject.FindGameObjectWithTag("Player");

        if(Target.GetComponent<PlayerController>() == null)
        {

            Target = GameObject.FindGameObjectWithTag("Mplayer");

        }

        if (Target != null)
        {
            canfollow = false;
            for(int i = 0; i <  listofplayers.Length; i++)
            {
                if (Target.GetComponent<PlayerController>() != null)
                {
                    if (listofplayers[i] == Target.GetComponent<PlayerController>().upgradeLevel)
                    {
                        canfollow = true;
                    }
                }
                if(Target.GetComponent<PMultiplayerController>() != null)
                {
                    if (listofplayers[i] == Target.GetComponent<PMultiplayerController>().upgradeLevel)
                    {
                        canfollow = true;
                    }
                }
            }

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
      
        if(transform.position.x > 26 || transform.position.x < -24 || transform.position.y > 22 || transform.position.y < -19)
        {

            Destroy(gameObject);

        }


    }


    IEnumerator walkDirection()
    {
        direction.x = Random.Range(-0.5f, 0.5f);
        direction.y = Random.Range(-0.5f, 0.5f);
        yield return new WaitForSeconds(Random.Range(1.1f, 2.2f));      
        canWalk = false;
    }

}
