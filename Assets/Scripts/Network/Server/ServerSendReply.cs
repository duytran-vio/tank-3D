using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;

public class ServerSendReply : ApiSingleton<ServerSendReply>
{
    public void ReplyRegister(int desId, Dictionary<int, TankInfo> tanks){
        List<object> data = new List<object>();
        data.Add(desId);
        data.Add(tanks.Count);
        foreach(var item in tanks){
            data.Add(item.Key);
            data.Add(item.Value.position);
            data.Add(item.Value.turretAngle);
            data.Add(item.Value.HP);
            data.Add(item.Value.damage);
        }
        object[] content = data.ToArray();
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        PhotonNetwork.RaiseEvent((byte)EventCode.REGISTER, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public void ReplyMoveEvent(int id, Vector3 newPos){
        object[] content = new object[] {id, newPos};
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        PhotonNetwork.RaiseEvent((byte)EventCode.MOVE, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public void ReplyMoveTurretEvent(int id, float r){
        object[] content = new object[] {id, r};
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        PhotonNetwork.RaiseEvent((byte)EventCode.MOVETURRET, content, raiseEventOptions, SendOptions.SendReliable);
    }
}
