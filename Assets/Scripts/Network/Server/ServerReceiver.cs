using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;
public class ServerReceiver : MonoBehaviour
{
    //public const byte (byte)NetworkEvent.PositionEventCode = 1;

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        if (eventCode == (byte)EventCode.REGISTER){
            HandleRegisterEvent(photonEvent);
        }
    }

    private void HandleRegisterEvent(EventData photonEvent){
        object[] data = (object[])photonEvent.CustomData;
        int userId = (int)data[0];
        ServerManager.Instance.Register(userId);
    }
}

