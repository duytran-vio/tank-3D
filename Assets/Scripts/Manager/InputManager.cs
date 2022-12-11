using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    public float speed;
    public float turrentSpeed;
    TankInfo mainTankInfo;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Init(){
        mainTankInfo = GameManager.Instance.GetMainTankInfo();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();
        if (Input.GetKey(KeyCode.K)){
            GameManager.Instance.SetMainTankTurrent(mainTankInfo.turrentAngle - turrentSpeed);
        }
        if (Input.GetKey(KeyCode.L)){
            GameManager.Instance.SetMainTankTurrent(mainTankInfo.turrentAngle + turrentSpeed);
        }
    }

    private void HandleMovementInput(){
        float verticalInput = Input.GetAxisRaw("Vertical"); 
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 moveDir = new Vector3(horizontalInput, 0, verticalInput) * Time.deltaTime * speed;

        GameManager.Instance.MoveMainTank(mainTankInfo.position + moveDir);
    }
}
