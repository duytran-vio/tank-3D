using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    private GameObject tankPrefab;

    private GameObject _tankContainer;

    void Start()
    {
        tankPrefab = Resources.Load<GameObject>("Prefabs/Tank");
        _tankContainer = GameObject.Find("Tanks");
        if (_tankContainer == null){
            _tankContainer = new GameObject("Tanks");
        }
    }

    public Transform SpawnTank(Vector3 position){
        GameObject newTank = Instantiate(tankPrefab, position, Quaternion.identity);
        newTank.transform.SetParent(_tankContainer.transform);
        return newTank.transform;
    }
}
