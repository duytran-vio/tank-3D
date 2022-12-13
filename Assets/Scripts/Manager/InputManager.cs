using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    public float speed;
    public float turretSpeed;
    TankInfo mainTankInfo;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Init(TankInfo tankInfo)
    {
        mainTankInfo = tankInfo;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();
        if (Input.GetKey(KeyCode.J))
        {
            GameManager.Instance.SetMainTankTurret(mainTankInfo.turretAngle - turretSpeed);
        }
        if (Input.GetKey(KeyCode.L))
        {
            GameManager.Instance.SetMainTankTurret(mainTankInfo.turretAngle + turretSpeed);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameManager.Instance.FireMainTank(mainTankInfo.turretAngle);
        }
    }

    private void HandleMovementInput()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 moveInput = new Vector3(horizontalInput, 0, verticalInput);
        //moveDir = Quaternion.Euler(0, mainTankInfo.hullAngle, 0) * moveDir;

        GameManager.Instance.MoveMainTank(moveInput);
    }
}
