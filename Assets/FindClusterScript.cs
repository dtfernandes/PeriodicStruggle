using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClusterScript : MonoBehaviour {


    public GameObject[] clusters;
    public List <GameObject> typeCluster;

    public float rotationSpeed;
    public float minDist;

    public Sprite hArrow, cArrow, oArrow, nArrow;
    public GameObject hObject, cObject, oObject, nObject;

    public GameObject arrow;

    public GameObject player;
    public GameObject selectedAtom, selectedCluster;

    int atomToFollow;

    bool useQandE;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        player = GameObject.FindGameObjectWithTag("Player");

        for (var i = typeCluster.Count - 1; i > -1; i--)
        {
            if (typeCluster[i] == null)
                typeCluster.RemoveAt(i);
        }

        if (player != null)
        {
            transform.position = player.transform.position;

            bool existscluster = false;
            foreach (GameObject cluster in GameObject.FindGameObjectsWithTag("atomo"))
            {
                if (cluster.GetComponent<AtomosRandomRotation>() != null)
                {
                    if (selectedAtom == cluster.GetComponent<AtomosRandomRotation>().atomosSpawn && !typeCluster.Contains(cluster))
                    {

                        typeCluster.Add(cluster);

                    }
                    if (selectedAtom != cluster.GetComponent<AtomosRandomRotation>().atomosSpawn && typeCluster.Contains(cluster))
                    {

                        typeCluster.Remove(cluster);

                    }
                   

                }

                if (cluster.GetComponent<EnemyCluster>() != null)
                {

                    if (selectedAtom == cluster.GetComponent<EnemyCluster>().atomosSpawn && !typeCluster.Contains(cluster))
                    {

                        typeCluster.Add(cluster);

                    }
                    if (selectedAtom != cluster.GetComponent<EnemyCluster>().atomosSpawn && typeCluster.Contains(cluster))
                    {

                        typeCluster.Remove(cluster);

                    }


                }

                

            }

            foreach (GameObject clustertype in typeCluster)
            {
                if (clustertype != null)
                {

                    if (minDist == Vector2.Distance(clustertype.transform.position, player.transform.position))
                    {
                        existscluster = true;
                    }
        
                    if (Vector2.Distance(clustertype.transform.position, player.transform.position) < minDist )
                    {
                        minDist = Vector2.Distance(clustertype.transform.position, player.transform.position);
                        selectedCluster = clustertype;
                        existscluster = true;   
                    }

                    if (minDist != Vector2.Distance(clustertype.transform.position, player.transform.position) && !existscluster)
                    {

                        minDist = 999;
                        selectedCluster = null;
                       
                    }

                }
            }

            //Rotation
            if (selectedCluster != null && existscluster)
            {
                if (minDist > 2.5f)
                {
                    float AngleRad = Mathf.Atan2(selectedCluster.transform.position.x - transform.position.x, selectedCluster.transform.position.y - transform.position.y);
                    float AngleDeg = (180 / Mathf.PI) * AngleRad;
                    transform.rotation = new Quaternion(Mathf.LerpAngle(transform.rotation.x, Quaternion.Euler(0, 0, -AngleDeg + 90).x, rotationSpeed * Time.deltaTime), Mathf.LerpAngle(transform.rotation.y, Quaternion.Euler(0, 0, -AngleDeg + 90).y, rotationSpeed * Time.deltaTime), Mathf.LerpAngle(transform.rotation.z, Quaternion.Euler(0, 0, -AngleDeg + 90).z, rotationSpeed * Time.deltaTime), Mathf.LerpAngle(transform.rotation.w, Quaternion.Euler(0, 0, -AngleDeg + 90).w, rotationSpeed * Time.deltaTime));
                    //  (transform.rotation, Quaternion.Euler(0, 0, -AngleDeg + 90),0);
                }
            }
        }

        if(minDist <= 2)
        {
            arrow.SetActive(false);           
        }
        if (minDist > 2.5f)
        {
            arrow.SetActive(true);
        }

        if(typeCluster.Count == 0)
        {

            arrow.SetActive(false);

        }

        if (typeCluster.Count > 0 && minDist > 2.5f)
        {

            arrow.SetActive(true);

        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            useQandE = true;
            atomToFollow++;
            if (atomToFollow > 4)
            {
                atomToFollow = 1;
            }
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            useQandE = true;
            atomToFollow--;
            if(atomToFollow < 1)
            {
                atomToFollow = 4;
            }
        }
        if(useQandE)
        {
            switch (atomToFollow)
            {
                case 1:
                    FindH();
                    break;
                case 2:
                    FindO();
                    break;
                case 3:
                    FindC();
                    break;
                case 4:
                    FindN();
                    break;
            }
        }
    }

    public void FindH()
    {
        useQandE = false;
        if (arrow.GetComponent<SpriteRenderer>().sprite != hArrow)
        {
            arrow.GetComponent<SpriteRenderer>().sprite = hArrow;
        }
        else
        {
            arrow.GetComponent<SpriteRenderer>().sprite = null;
        }
        selectedAtom = hObject;

    }
    public void FindO()
    {
        useQandE = false;
        if (arrow.GetComponent<SpriteRenderer>().sprite != oArrow)
        {
            arrow.GetComponent<SpriteRenderer>().sprite = oArrow;
        }
        else
        {
            arrow.GetComponent<SpriteRenderer>().sprite = null;
        }
        selectedAtom = oObject;

    }
    public void FindC()
    {
        useQandE = false;
        if (arrow.GetComponent<SpriteRenderer>().sprite != cArrow)
        {
            arrow.GetComponent<SpriteRenderer>().sprite = cArrow;
        }
        else
        {
            arrow.GetComponent<SpriteRenderer>().sprite = null;
        }
        selectedAtom = cObject;

    }
    public void FindN()
    {
        useQandE = false;
        if (arrow.GetComponent<SpriteRenderer>().sprite != nArrow)
        {
            arrow.GetComponent<SpriteRenderer>().sprite = nArrow;
        }
        else
        {
            arrow.GetComponent<SpriteRenderer>().sprite = null;
        }
        selectedAtom = nObject;

    }

}
