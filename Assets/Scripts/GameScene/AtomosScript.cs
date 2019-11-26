using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomosScript : MonoBehaviour
{

    public GameObject sliders;
    public GameObject player;
    public GameObject pickUpSound;
    public GameObject score;

	void Start ()
    {
        pickUpSound = GameObject.FindWithTag("Sound");
        sliders = GameObject.FindGameObjectWithTag("Sliders");
        score = GameObject.FindGameObjectWithTag("Score");
	}
	
	void Update ()
    {
        player = GameObject.FindGameObjectWithTag("Player");   
        
         foreach(Collider2D coll in player.GetComponents<Collider2D>())
         {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), coll);
         }                       
    }
	

    void OnTriggerEnter2D(Collider2D collision)
    {        
        if(collision.gameObject.tag == "Player")
        {
            if (sliders.GetComponent<SliderManager>() != null)
            {
                switch (gameObject.name)
                {
                    case "Hidrogenio(Clone)":
                        sliders.GetComponent<SliderManager>().values[0] += 1;
                        score.GetComponent<ScoreManager>().score++;
                        break;
                    case "Oxigenio(Clone)":
                        sliders.GetComponent<SliderManager>().values[1] += 1;
                        score.GetComponent<ScoreManager>().score++;
                        break;
                    case "Carbono(Clone)":
                        sliders.GetComponent<SliderManager>().values[2] += 1;
                        score.GetComponent<ScoreManager>().score++;
                        break;
                    case "Nitrogenio(Clone)":
                        sliders.GetComponent<SliderManager>().values[3] += 1;
                        score.GetComponent<ScoreManager>().score++;
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
                    case "Nitrogenio(Clone)":
                        sliders.GetComponent<SliderManagerMultiplayer>().values[3] += 1;
                        break;
                }
            }
            pickUpSound.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }       
    }

    
}
