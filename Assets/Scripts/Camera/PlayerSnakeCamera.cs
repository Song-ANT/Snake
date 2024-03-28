using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSnakeCamera : MonoBehaviour
{
    public Transform followTarget;
    public Vector3 offset;
    public Vector3 lookDirection;


    public void Initialized(Transform player)
    {
        followTarget = player;
        offset = new Vector3(0, 20, 0);
        lookDirection = Vector3.down;

    }


    void LateUpdate()
    {
        if (followTarget == null) return;

        transform.position = followTarget.position + offset; // 카메라 위치 조정
        transform.rotation = Quaternion.LookRotation(lookDirection); // 카메라 회전 고정
    }
}
