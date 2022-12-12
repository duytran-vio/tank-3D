using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class JoinServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();   
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.CreateRoom("1");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MultiPlayers");
    }
}
