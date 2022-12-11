using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public float turrentRotateSpeed;
    private Transform tankTurrent;
    void Awake(){
        tankTurrent = transform.Find("TankRenderers/TankTurret");
    }
    
    public void HandleMovementToPosition(Vector3 targetPosition){
       Vector3 _moveDirection = targetPosition - transform.position;
    //    if (Vector3.Distance(targetPosition, transform.position) < 0.5f) return;
       _moveDirection.y = 0;
       Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
       transform.position = newPosition;
       _moveDirection.Normalize();

       HandleRotation(_moveDirection);
    }

    public void HandleTurrentAngle(float r){
        Quaternion targetRotation = Quaternion.Euler(0, r, 0);
        Quaternion newRotation = Quaternion.Lerp(tankTurrent.rotation, targetRotation, Time.deltaTime * turrentRotateSpeed);
        tankTurrent.rotation = targetRotation;
    }

    private void HandleRotation(Vector3 _moveDirection){
        if (_moveDirection.magnitude == 0) return;
        Quaternion targetRotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
        Quaternion newRotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        transform.rotation = newRotation;
    }
}
