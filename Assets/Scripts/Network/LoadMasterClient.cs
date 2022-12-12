using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LoadMasterClient : MonoBehaviour
{
    private GameObject client;
    private GameObject serverManager;
    // Start is called before the first frame update
    void Awake()
    {
        client = GameObject.Find("Client");
        serverManager = GameObject.Find("ServerManager");
        if (PhotonNetwork.IsMasterClient){
            client.SetActive(false);
            serverManager.SetActive(true);
        }
        else{
            client.SetActive(true);
            serverManager.SetActive(false);
        }
    }
}
