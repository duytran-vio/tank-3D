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

    public void Init(TankInfo tankInfo){
        mainTankInfo = tankInfo;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();
        if (Input.GetKey(KeyCode.K)){
            GameManager.Instance.SetMainTankTurret(mainTankInfo.turretAngle - turretSpeed);
        }
        if (Input.GetKey(KeyCode.L)){
            GameManager.Instance.SetMainTankTurret(mainTankInfo.turretAngle + turretSpeed);
        }
        if (Input.GetKeyDown(KeyCode.J)){
            GameManager.Instance.FireMainTank(mainTankInfo.turretAngle);
        }
    }

    private void HandleMovementInput(){
        float verticalInput = Input.GetAxisRaw("Vertical"); 
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (verticalInput == 0 && horizontalInput == 0) return;
        Vector3 moveDir = new Vector3(horizontalInput, 0, verticalInput) * Time.deltaTime * speed;
        
        GameManager.Instance.MoveMainTank(mainTankInfo.position + moveDir);
    }
}
