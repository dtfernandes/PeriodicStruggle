using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    //PlayerProperties
    public float speed;
    public float fireRate;
    public int upgradeLevel; //Diz qual o upgrade que o Player tem atualmente
    public int maxLives;

    public int currentLives;
    public bool canfire;
    private Rigidbody2D myRigidbody;
    public GameObject electrons;
 

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemys")
        {
            if (collision.gameObject.GetComponent<EnemyScript>().canfollow)
            {
                Destroy(GameObject.FindGameObjectsWithTag("electrons")[0]);
                currentLives -= 1;
                Destroy(collision.gameObject);
            }
        }

    }


    // Use this for initialization
    void Start()
    {
        for(int i = 0; i < maxLives; i++)
        {
            if (GameObject.FindGameObjectsWithTag("electrons").Length < maxLives)
            {
                Instantiate(electrons, transform.position, Quaternion.identity);
            }
        }
        currentLives = maxLives; 
        myRigidbody = GetComponent<Rigidbody2D>();

    }


    // Update is called once per frame
    void Update()
    {

        if(currentLives <= 0)
        {

            SceneManager.LoadScene("GameOverScene");

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
        float AngleRad = Mathf.Atan2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        transform.rotation = Quaternion.Euler(0, 0, -AngleDeg + 90);

        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            if (canfire)
            {
                foreach (GameObject gun in GameObject.FindGameObjectsWithTag("Gun"))
                {

                    StartCoroutine(gun.GetComponent<GunScript>().FireBullet());


                }
                canfire = false;
                gameObject.GetComponent<AudioSource>().Play();
            }
        }
    }
}
