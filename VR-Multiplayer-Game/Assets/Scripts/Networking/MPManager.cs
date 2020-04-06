using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class MPManager : MonoBehaviourPunCallbacks {

    public GameObject[] EnableObjectsOnConnect;
    public GameObject[] DisableOjectsOnConnect;
    public GameObject map;

    void Start() {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() {
        foreach(GameObject obj in EnableObjectsOnConnect) {
            obj.SetActive(true);
        }

        foreach(GameObject obj in DisableOjectsOnConnect) {
            obj.SetActive(false);
        }

        Debug.Log("We are connected");
    }

    public void JoinNormalGame() {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message) {
        CreateNormalGame();
    }

    public void CreateNormalGame() {
        PhotonNetwork.AutomaticallySyncScene = true;

        RoomOptions ro = new RoomOptions {MaxPlayers = 2, IsOpen = true, IsVisible = true};
        PhotonNetwork.CreateRoom("defaultNormal", ro, TypedLobby.Default);
    }

    public override void OnJoinedRoom() {
        SceneManager.LoadScene("Normal");
    }

}
