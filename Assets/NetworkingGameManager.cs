using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NetworkingGameManager : MonoBehaviour
{
    public float enemytimer; //tempo entre cada spawn de inimigosS
    public string[] Enemy;



    public bool canspawn;
    public int enemylimit;
    public int numSpawnMax;
    public GameObject atomosCluster;
    public GameObject[] atomos;
    public Vector3 position, selfPos;


    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(enemytimer);

        PhotonNetwork.Instantiate(Enemy[Random.Range(0, Enemy.Length)], position, Quaternion.identity, 0);

        if (GameObject.FindGameObjectsWithTag("Enemys").Length < enemylimit)
        {
            StartCoroutine("SpawnEnemy");
        }
        else
        {

            canspawn = true;

        }
    }



    void Start()
    {
        if (PhotonNetwork.isMasterClient)
        {

            StartCoroutine("SpawnEnemy");
        }
    }

    void Update()
    {


        if (PhotonNetwork.isMasterClient)
        {
            if (GameObject.FindGameObjectsWithTag("Enemys").Length < enemylimit && canspawn)
            {

                StartCoroutine("SpawnEnemy");
                canspawn = false;

            }

            atomos = GameObject.FindGameObjectsWithTag("atomo");

            position = new Vector3(Random.Range(-23, 25), Random.Range(-18, 20), 0);
            if (atomos.Length <= numSpawnMax)
            {



                GameObject atomC = PhotonNetwork.Instantiate(atomosCluster.name, position, Quaternion.identity,0);

                atomC.GetComponent<AtomosRandomRNetworking>().atomNumb = GetAtomNumber();
            }

        }
     
       
    }


    public int GetAtomNumber()
    {
        GameObject[] hidro = GameObject.FindGameObjectsWithTag("Hidrogenio");
        GameObject[] oxi = GameObject.FindGameObjectsWithTag("Oxigenio");
        GameObject[] carb = GameObject.FindGameObjectsWithTag("Carbono");

        int[] lenghtMolecules = new int[] { hidro.Length, oxi.Length, carb.Length };

        return lenghtMolecules.ToList().IndexOf(lenghtMolecules.Min());

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

