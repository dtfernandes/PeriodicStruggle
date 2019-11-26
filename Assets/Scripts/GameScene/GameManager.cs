using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour {

    public float enemytimer; //tempo entre cada spawn de inimigosS
    public GameObject[] Enemy;


    public bool canspawn;
    public int enemylimit;
    public int numSpawnMax;
    public GameObject atomosCluster;
    public GameObject[] atomos;
    public Vector3 position;


    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(enemytimer);
        Instantiate(Enemy[Random.Range(0,Enemy.Length)], position, Quaternion.identity);
        if (GameObject.FindGameObjectsWithTag("Enemys").Length < enemylimit)
        {
            StartCoroutine("SpawnEnemy");
        }
        else
        {

            canspawn = true;

        }
    }
    


	void Start ()
    {

        StartCoroutine("SpawnEnemy");

    }
	
	void Update ()
    {

       

        if (GameObject.FindGameObjectsWithTag("Enemys").Length < enemylimit && canspawn)
        {

            StartCoroutine("SpawnEnemy");
            canspawn = false;

        }
       /* if(Input.GetKey(KeyCode.Mouse1))
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }*/

        atomos = GameObject.FindGameObjectsWithTag("atomo");
        position = new Vector3(Random.Range(-23, 25), Random.Range(-18, 20), 0);
        if (atomos.Length <= numSpawnMax)
        {

            

            GameObject atomC = Instantiate(atomosCluster, position, Quaternion.identity);
            
            atomC.GetComponent<AtomosRandomRotation>().atomNumb = GetAtomNumber();
        }
	}


    public int GetAtomNumber()
    {
        GameObject[] hidro = GameObject.FindGameObjectsWithTag("Hidrogenio");
        GameObject[] oxi = GameObject.FindGameObjectsWithTag("Oxigenio");
        GameObject[] carb = GameObject.FindGameObjectsWithTag("Carbono");
        GameObject[] nitr = GameObject.FindGameObjectsWithTag("Nitrogenio");

        int[] lenghtMolecules = new int[] { hidro.Length, oxi.Length, carb.Length, nitr.Length };

        int ola = lenghtMolecules.ToList().IndexOf(lenghtMolecules.Min());
        return ola;

    }


}
