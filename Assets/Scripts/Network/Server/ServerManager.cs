using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerManager : MonoSingleton<ServerManager>
{
    private Dictionary<int, TankInfo> _tanks;

    void Start(){
        _tanks = new Dictionary<int, TankInfo>();
    }

    private Vector3 GetRandomPosition(){
        return Vector3.zero;
    }

    public void Register(int newId){
        TankInfo tankInfo = new TankInfo(newId);
        tankInfo.position = GetRandomPosition();
        _tanks.Add(newId, tankInfo);
        ServerSendReply.Instance.ReplyRegister(newId, _tanks);
    }

    public void Move(int id, Vector3 newPos){
        _tanks[id].position += newPos;
        ServerSendReply.Instance.ReplyMoveEvent(id, _tanks[id].position);
    }

    public void MoveTurret(int id, float r){
        _tanks[id].turretAngle += r;
        ServerSendReply.Instance.ReplyMoveTurretEvent(id, _tanks[id].turretAngle);
    }
}
