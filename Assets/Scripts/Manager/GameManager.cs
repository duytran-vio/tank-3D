using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private List<Transform> _tanks;
    private int mainTankIndex;

    void Start(){
        _tanks = new List<Transform>();
        AddNewTank(Vector3.zero);
        mainTankIndex = 0;
        Init();
        AddNewTank(new Vector3(5, 0, 5));
    }

    void Init(){
        InputManager.Instance.Init(GetMainTankInfo());
        CameraManager.Instance.Init(_tanks[mainTankIndex]);
    }

    public Transform AddNewTank(Vector3 position, int newId = -1){
        if (newId == -1){
            newId = _tanks.Count;
        }
        Transform newTank = SpawnManager.Instance.SpawnTank(position);
        _tanks.Add(newTank.transform);
        newTank.GetComponent<TankManager>().Init(newId);
        return newTank.transform;
    }

    public TankInfo GetMainTankInfo(){
        return GetTankInfo(mainTankIndex);
    }

    public TankInfo GetTankInfo(int index){
        if (index >= _tanks.Count) return null;
        return _tanks[index].GetComponent<TankManager>().tankInfo;
    }

    public void MoveMainTank(Vector3 position){
        MoveTank(mainTankIndex, position);
    }

    public void SetMainTankTurret(float angle){
        SetTankTurret(mainTankIndex, angle);
    }

    public void FireMainTank(float angle){
        FireTank(mainTankIndex, angle);
    }

    public void FireTank(int index, float angle){
        if (index >= _tanks.Count) return;
        _tanks[index].GetComponent<TankManager>().Fire(angle);
    }

    public void MoveTank(int index, Vector3 position){
        if (index >= _tanks.Count) return;
        _tanks[index].GetComponent<TankManager>().SetPosition(position);
    }

    public void SetTankTurret(int index, float angle){
        if (index >= _tanks.Count) return;
        _tanks[index].GetComponent<TankManager>().SetTurretAngle(angle);
    }

    public int GetTankIndex(int id){
        for(int i = 0; i < _tanks.Count; i++){
            if (_tanks[i].GetComponent<TankManager>().tankInfo.id == id){
                return i;
            }
        }
        return -1;
    }

    public void TankDie(int index){
        if (index >= _tanks.Count) return;
        _tanks[index].GetComponent<TankManager>().Die();
    }

    public void Hit(int fromId, int toId){
        int sourceIndex = GetTankIndex(fromId);
        int desIndex = GetTankIndex(toId);

        TankInfo sourceInfo = GetTankInfo(sourceIndex);
        TankInfo desInfo = GetTankInfo(desIndex);
        Debug.Log(desInfo.HP);

        if (sourceInfo.damage > desInfo.HP){
            TankDie(desIndex);
        }

        desInfo.HP -= sourceInfo.damage;
    }
}
