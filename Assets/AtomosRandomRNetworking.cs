using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomosRandomRNetworking : MonoBehaviour
{
    public GameObject[] atomos;             //Lista de átomos a fazer spawn, no caso de estar associado a um Enemy, atomos.Length = 0
    public GameObject atomosSpawn;          //Átomos a fazer spawn (Óxigenio, Hidrogénio ou Carbono)  
    public GameObject atomosS;
    public int atomNumb;
    bool clusterSpawned;

    void Start()
    {
        if (PhotonNetwork.isMasterClient)
        {
            CreateCluster();
        }
    }

    void LateUpdate()
    {
        if (transform.childCount >= 1)
        {
            clusterSpawned = true;
        }
        if (transform.childCount == 0 && clusterSpawned)
        {
          PhotonNetwork.Destroy(gameObject);
        }
    }


    public void CreateCluster()
    {
        atomosSpawn = atomos[atomNumb];
        for (int i = 0; i < 5; i++)
        {
            atomosS = PhotonNetwork.Instantiate(atomosSpawn.name, new Vector3(Random.Range(transform.localPosition.x + 0.0f, transform.localPosition.x + 1.5f), Random.Range(transform.localPosition.y + 0.0f, transform.localPosition.y + 1.5f), 0), Quaternion.identity, 0);
            atomosS.transform.parent = gameObject.transform;
        }
    }
}
