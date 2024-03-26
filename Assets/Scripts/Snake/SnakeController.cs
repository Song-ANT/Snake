using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private float _betweenDistance = 0.2f;
    private List<GameObject> snakeBody = new List<GameObject>();

    private int _level = 1;
    private float _countUp = 0f;

    private void Start()
    {
        CreateBodyParts();
    }

    private void FixedUpdate()
    {
        if(_level > snakeBody.Count)
        {
            CreateBodyParts();
        }
        SnakeBodyMovement();
    }

    private void SnakeBodyMovement()
    {


        if(snakeBody.Count > 1)
        {
            for(int i=1; i<snakeBody.Count; i++) 
            {
                SnakeMarker mark = snakeBody[i-1].GetComponent<SnakeMarker>();
                snakeBody[i].transform.position = mark.markerList[0].position;
                snakeBody[i].transform.rotation = mark.markerList[0].rotation;
                mark.markerList.RemoveAt(0);
            }
        }
    }

    private void CreateBodyParts()
    {
        if(snakeBody.Count == 0)
        {
            GameObject temp1 = Main.Resource.InstantiatePrefab("SnakeHead", transform);
            temp1.tag = "Head";

            if (!temp1.GetComponent<SnakeMarker>())
            {
                temp1.AddComponent<SnakeMarker>();
            }

            if (!temp1.GetComponent<Rigidbody>())
            {
                temp1.AddComponent<Rigidbody>();
                temp1.GetComponent<Rigidbody>().useGravity = false;
            }
            snakeBody.Add(temp1);
            return;
        }


        SnakeMarker mark = snakeBody[snakeBody.Count - 1].GetComponent<SnakeMarker>();

        if(_countUp == 0) 
        {
            mark.ClearMarkerList();
        }

        _countUp += Time.deltaTime;
        if (_countUp > _betweenDistance)
        {
            GameObject temp = Main.Resource.InstantiatePrefab("SnakeBody", transform);
            if (!temp.GetComponent<SnakeMarker>())
            {
                temp.AddComponent<SnakeMarker>();
            }

            if (!temp.GetComponent<Rigidbody>())
            {
                temp.AddComponent<Rigidbody>();
                temp.GetComponent<Rigidbody>().useGravity = false;
            }
            snakeBody.Add(temp);
            temp.GetComponent<SnakeMarker>().ClearMarkerList();
            _countUp = 0;
        }
    }

    public void AddBodyParts()
    {
        //CreateBodyParts();
        _level++;
    }

    public Transform GetSnakehead()
    {
        return snakeBody[0].transform;
    }
}
