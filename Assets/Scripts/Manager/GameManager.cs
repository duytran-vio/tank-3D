using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private Dictionary<int, TankManager> _tanks;
    public int mainTankIndex;
    private bool _isOnline;

    public Dictionary<int, TankManager> TanksInScene => _tanks;

    void Start()
    {
        _tanks = new Dictionary<int, TankManager>();
        
        _isOnline = PlayerPrefs.GetInt("isOnline") == 1;
        if (!_isOnline){
            AddNewTank(Vector3.zero);
	        mainTankIndex = 0;
	        Init();
	        BotManager.Instance.SetTankAsBot(AddNewTank(new Vector3(-8f, 0, -8f))); // test
        }
        else{
            Random.InitState(System.DateTime.Now.Millisecond);
            mainTankIndex = Random.Range(0, 10000);
            ClientSendRequest.Instance.SendRegisterRequest(mainTankIndex);
        }
    }

    public void Init()
    {
        InputManager.Instance.Init(GetMainTankInfo());
        CameraManager.Instance.Init(_tanks[mainTankIndex].transform);
    }

    public TankManager AddNewTank(Vector3 position, int newId = -1)
    {
        if (newId == -1)
        {
            newId = _tanks.Count;
        }
        Transform newTank = SpawnManager.Instance.SpawnTank(position);
        _tanks.Add(newId, newTank.GetComponent<TankManager>());
        _tanks[newId].Init(newId);
        return _tanks[newId];
    }

    public TankManager AddNewTank(int newId = -1)
    {
        if (newId == -1)
        {
            newId = _tanks.Count;
        }
        Transform newTank = SpawnManager.Instance.SpawnTank();
        _tanks.Add(newId, newTank.GetComponent<TankManager>());
        _tanks[newId].Init(newId);
        return _tanks[newId];
    }

    public TankInfo GetMainTankInfo()
    {
        return GetTankInfo(mainTankIndex);
    }

    public TankInfo GetTankInfo(int index)
    {
        if (!_tanks.ContainsKey(index)) return null;
        return _tanks[index].tankInfo;
    }

    public void MoveMainTank(Vector3 position)
    {
        MoveTank(mainTankIndex, position);
    }

    public void SetMainTankTurret(float angle)
    {
        SetTankTurret(mainTankIndex, angle);
    }

    public void FireMainTank(float angle)
    {
        FireTank(mainTankIndex, angle);
    }

    public void FireTank(int index, float angle)
    {
        if (!_tanks.ContainsKey(index)) return;
        _tanks[index].Fire(angle);
    }

    public void MoveTank(int index, Vector3 position)
    {
        if (!_tanks.ContainsKey(index)) return;
        _tanks[index].SetMovementInput(position);
    }

    public void SetTankTurret(int index, float angle)
    {
        if (!_tanks.ContainsKey(index)) return;
        _tanks[index].SetTurretAngle(angle);
    }

    public void TankDie(int index)
    {
        if (!_tanks.ContainsKey(index)) return;
        _tanks[index].Die();
    }

    public void Hit(int fromId, int toId)
    {
        TankInfo sourceInfo = GetTankInfo(fromId);
        TankInfo desInfo = GetTankInfo(toId);

        if (sourceInfo.damage >= desInfo.HP)
        {
            TankDie(toId);
        }

        // desInfo.HP -= sourceInfo.damage;
        _tanks[toId].SetHP(desInfo.HP - sourceInfo.damage);
    }
}
