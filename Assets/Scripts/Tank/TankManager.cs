using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour
{
    public TankInfo tankInfo;
    private TankMovement tankMovement;
    // Start is called before the first frame update
    void Awake()
    {
        tankInfo = new TankInfo();
        tankInfo.position = transform.position;
        tankInfo.turrentAngle = 0;
        tankMovement = GetComponent<TankMovement>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tankMovement.HandleMovementToPosition(tankInfo.position);
        tankMovement.HandleTurrentAngle(tankInfo.turrentAngle);
    }

    public void SetPosition(Vector3 position){
        tankInfo.position = position;
    }

    public void SetTurrentAngle(float angle){
        tankInfo.turrentAngle = angle;
    }
}
