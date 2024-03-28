using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineManager 
{
    CinemachineVirtualCamera _playerCamera;

    

    public void SetPlayerSnakeCamera(Transform player)
    {
        var playerCameraObject = Main.Resource.InstantiatePrefab(Define.PrefabName.playerSnakeCamera);
        _playerCamera = playerCameraObject.GetComponent<CinemachineVirtualCamera>();
        var playerSnakeCamera = _playerCamera.GetComponent<PlayerSnakeCamera>();
        playerSnakeCamera.Initialized(player);


        
    }
}
