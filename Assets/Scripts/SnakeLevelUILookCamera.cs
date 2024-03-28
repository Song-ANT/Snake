using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeLevelUILookCamera : MonoBehaviour
{
    private Transform _target;

    private void Awake()
    {
        _target = Camera.main.transform;
        var dir = _target.forward;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    private void Update()
    {
        var dir = _target.forward;
        transform.rotation = Quaternion.LookRotation(dir);
    }

}
