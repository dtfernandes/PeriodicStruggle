using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerNetwork : MonoBehaviour
{
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "" + score;
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

}
