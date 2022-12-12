using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoSingleton<BotManager>
{
    private GameObject botControllerPrefab;


    private void Awake()
    {
        botControllerPrefab = Resources.Load<GameObject>("Prefabs/BotController");
    }

    public void SetTankAsBot(TankManager tank)
    {
        GameObject botControllerGO = Instantiate(botControllerPrefab, tank.transform);
        if (botControllerGO.TryGetComponent(out BotController controller))
        {
            controller.Init(tank);
        }
    }
}
