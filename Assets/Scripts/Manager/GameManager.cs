using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private List<Transform> _tanks;
    private int mainTankIndex;

    void Start(){
        _tanks = new List<Transform>();
        AddNewTank();
        mainTankIndex = 0;
        Init();
    }

    void Init(){
        InputManager.Instance.Init();
    }

    public Transform AddNewTank(){
        Transform newTank = SpawnManager.Instance.SpawnTank();
        _tanks.Add(newTank.transform);
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

    public void SetMainTankTurrent(float angle){
        SetTankTurrent(mainTankIndex, angle);
    }

    public void MoveTank(int index, Vector3 position){
        if (index >= _tanks.Count) return;
        _tanks[index].GetComponent<TankManager>().SetPosition(position);
    }

    public void SetTankTurrent(int index, float angle){
        if (index >= _tanks.Count) return;
        _tanks[index].GetComponent<TankManager>().SetTurrentAngle(angle);
    }
}
