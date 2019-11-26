using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class photonHandler : MonoBehaviour {

    public PhotonButtons photonB;
    public GameObject mainPlayer;

    public void Awake()
    {
        DontDestroyOnLoad(transform);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;

        PhotonNetwork.sendRate = 30;
        PhotonNetwork.sendRateOnSerialize = 20;
    }

    public void createNewRoom()
    {

        PhotonNetwork.CreateRoom(photonB.createRoomInput.text, new RoomOptions() { MaxPlayers = 4 }, null);

    }

    private void OnJoinedRoom()
    {
        moveScene();


    }

    public void joinOrCreateRoom()
    {

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(photonB.joinRoomInput.text, roomOptions, TypedLobby.Default);


    }

    public void moveScene()
    {
        PhotonNetwork.LoadLevel("MultiplayerGameScene");

    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MultiplayerGameScene")
        {
            spawnPlayer();

        }


    }

    private void spawnPlayer()
    {
        PhotonNetwork.Instantiate(mainPlayer.name, mainPlayer.transform.position, mainPlayer.transform.rotation, 0);
    }

}
