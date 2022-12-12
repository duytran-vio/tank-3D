using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    private Transform followTarget;
    public void Init(Transform target){
        followTarget = target;
    }

    void LateUpdate(){
        if (followTarget == null) return;
        transform.position = followTarget.position;
    }
}
