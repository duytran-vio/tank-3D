using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankMovement : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public float turretRotateSpeed;
    private Transform tankTurret;
    private NavMeshAgent navMeshAgent;
    void Awake()
    {
        tankTurret = transform.Find("TankRenderers/TankTurret");
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        navMeshAgent.angularSpeed = rotateSpeed;
    }

    public void HandleMovementToPosition(Vector3 targetPosition)
    {
        Vector3 _moveDirection = targetPosition - transform.position;
        //    if (Vector3.Distance(targetPosition, transform.position) < 0.1f) return;
        _moveDirection.y = 0;
        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime * speed);
        Vector3 movement = Vector3.Lerp(Vector3.zero, targetPosition - transform.position, Time.fixedDeltaTime * speed);
        navMeshAgent.Move(movement);
        //transform.position = newPosition;
        _moveDirection.Normalize();
        HandleRotation(_moveDirection);

        navMeshAgent.Move(movement);
    }

    public void HandleTurretAngle(float r)
    {
        Quaternion targetRotation = Quaternion.Euler(0, r, 0);
        Quaternion newRotation = Quaternion.Lerp(tankTurret.rotation, targetRotation, Time.fixedDeltaTime * turretRotateSpeed);
        tankTurret.rotation = targetRotation;
    }

    private void HandleRotation(Vector3 _moveDirection)
    {
        if (_moveDirection.magnitude == 0) return;
        Quaternion targetRotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
        Quaternion newRotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        transform.rotation = newRotation;
    }

    public Quaternion GetTurretRotation()
    {
        return tankTurret.rotation;
    }
}
