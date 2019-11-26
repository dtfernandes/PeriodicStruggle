using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonConnect : MonoBehaviour {
     public string versionname = "0.1";

    public GameObject Panel1, Panel2;

     void Awake()
     {
        PhotonNetwork.ConnectUsingSettings(versionname);

        Debug.Log("connecting");
 
     }





    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
       

    }

    private void OnJoinedLobby()
    {

        Debug.Log("On Joined Lobby");

        Panel1.SetActive(false);
        Panel2.SetActive(true);

    }

    private void OnDisconnectedFromPhoton()
    {
        Debug.Log("Disconnected Lobby");


        Panel2.SetActive(false);     
    }

    private void OnFailedToConnectToPhoton()
    {
        Debug.Log("Dis from photon services");
    }
}
