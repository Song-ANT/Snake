using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSnakeCamera : MonoBehaviour
{
    private Transform _player;
    private Vector3 _plusPosition;
    private Quaternion _plusRotation;

    public void Initialized(Transform player)
    {
        _player = player;
        _plusPosition = new Vector3(0, 15, -15);
        _plusRotation = Quaternion.Euler(new Vector3(-45, 0, 0));

    }

    private void Update()
    {
        if (_player != null)
        {
            transform.position = _player.position + _plusPosition;
            transform.rotation = Quaternion.Euler(new Vector3(-35, 0, 0));
        }
    }
}
