using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    public float speed;
    public float turretSpeed;
    TankInfo mainTankInfo;
    private bool isOnline;
    // Start is called before the first frame update
    void Start()
    {
        isOnline = PlayerPrefs.GetInt("isOnline") == 1;
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
            SetTankTurret(-turretSpeed);
        }
        if (Input.GetKey(KeyCode.L))
        {
            SetTankTurret(turretSpeed);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Fire(mainTankInfo.turretAngle);
        }
    }

    private void HandleMovementInput()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        
        if (verticalInput == 0 && horizontalInput == 0) return;
        Vector3 moveDir = new Vector3(horizontalInput, 0, verticalInput) * Time.deltaTime * speed;
        if (isOnline){
            ClientSendRequest.Instance.SendMoveTank(mainTankInfo.id, moveDir);
        }
        else{
           Vector3 moveInput = new Vector3(horizontalInput, 0, verticalInput) * speed * Time.deltaTime;
	        //moveDir = Quaternion.Euler(0, mainTankInfo.hullAngle, 0) * moveDir;

	        GameManager.Instance.MoveMainTank(mainTankInfo.position + moveInput);
        }
    }

    private void SetTankTurret(float r){
        if (isOnline){
            ClientSendRequest.Instance.SendMoveTurret(mainTankInfo.id, r);
        }
        else{
            GameManager.Instance.SetMainTankTurret(mainTankInfo.turretAngle + r);
        }
    }

    private void Fire(float r){
        if (isOnline){
            ClientSendRequest.Instance.SendTankFire(mainTankInfo.id, r);
        }
        else{
            GameManager.Instance.FireMainTank(r);
        }
    }
}
