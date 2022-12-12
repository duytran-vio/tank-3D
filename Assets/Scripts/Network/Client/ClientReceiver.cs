using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;

public class ClientReceiver : MonoBehaviour
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
        else if (eventCode == (byte)EventCode.MOVE){
            HandleMoveEvent(photonEvent);
        }
        else if (eventCode == (byte)EventCode.MOVETURRET){
            HandleMoveTurretEvent(photonEvent);
        }
    }

    private void HandleRegisterEvent(EventData photonEvent){
        object[] data = (object[])photonEvent.CustomData;
        int userId = (int)data[0];
        int n_tanks = (int)data[1];
        Dictionary<int, TankInfo> tanks = new Dictionary<int, TankInfo>();
        int k = 2;
        for(int i = 0; i < n_tanks; i++){
            int id = (int)data[k];
            TankInfo tankInfo = new TankInfo(id);
            tankInfo.position = (Vector3) data[k + 1];
            tankInfo.turretAngle = (float)data[k + 2];
            tankInfo.HP = (int)data[k + 3];
            tankInfo.damage = (int)data[k + 4];
            k += 5;
            tanks.Add(id, tankInfo);
        }
        if (userId == GameManager.Instance.mainTankIndex){
            foreach (var item in tanks){
                TankManager newTank = GameManager.Instance.AddNewTank(item.Value.position, item.Value.id);
                newTank.tankInfo = item.Value;
            }
            GameManager.Instance.Init();
        }
        else{
            TankManager newTank = GameManager.Instance.AddNewTank(tanks[userId].position, tanks[userId].id);
            newTank.tankInfo = tanks[userId];
        }
    }

    private void HandleMoveEvent(EventData photonEvent){
        object[] data = (object[])photonEvent.CustomData;
        int userId = (int)data[0];
        Vector3 newPos = (Vector3)data[1];
        GameManager.Instance.MoveTank(userId, newPos);
    }

    private void HandleMoveTurretEvent(EventData photonEvent){
        object[] data = (object[])photonEvent.CustomData;
        int userId = (int)data[0];
        float r = (float)data[1];
        GameManager.Instance.SetTankTurret(userId, r);
    }
}
