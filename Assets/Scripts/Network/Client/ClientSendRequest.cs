using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;

public class ClientSendRequest : ApiSingleton<ClientSendRequest>
{
    public void SendRegisterRequest(int newId){
        object[] content = new object[] {newId};
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.MasterClient };
        PhotonNetwork.RaiseEvent((byte)EventCode.REGISTER, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public void SendMoveTank(int id, Vector3 newPos){
        object[] content = new object[] {id, newPos};
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.MasterClient };
        PhotonNetwork.RaiseEvent((byte)EventCode.MOVE, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public void SendMoveTurret(int id, float r){
        object[] content = new object[] {id, r};
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.MasterClient };
        PhotonNetwork.RaiseEvent((byte)EventCode.MOVETURRET, content, raiseEventOptions, SendOptions.SendReliable);
    }
}
