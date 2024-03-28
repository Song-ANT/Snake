using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private float _betweenDistance = 0.2f;
    private List<GameObject> snakeBody = new List<GameObject>();

    private int _level = 1;
    private int _isEatFood = 0;
    private float _countUp = 0f;

    public event Action OnEatFoodEvent;

    private void Awake()
    {
        CreateBodyParts();
    }

    private void FixedUpdate()
    {
        if(_isEatFood>0)
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
            snakeBody.Add(temp);
            temp.GetComponent<SnakeMarker>().ClearMarkerList();
            temp.GetComponent<Snake>().SetSnakeBodyIndex(snakeBody.Count-1);
            _countUp = 0;
            _isEatFood -= 1;
        }


    }


    //public void 

    #region BeEatedMethod
    public void BeEatedHead()
    {
        Debug.Log($"{gameObject.name} : ¸Ó¸®¸ÔÈû");
        for(int i = snakeBody.Count - 1; i >= 0; i--)
        {
            Destroy(snakeBody[i]);
        }
        snakeBody.Clear();
        Destroy(gameObject);
    }

    public void BeEatedBody(int index)
    {
        Debug.Log($"{gameObject.name} : ¸öÅë¸ÔÈû");
        for (int i = snakeBody.Count - 1; i >= index; i--)
        {
            Destroy(snakeBody[i]);
            var pos = snakeBody[i].transform.position;
            var rot = snakeBody[i].transform.rotation;
            GameObject food = Main.Resource.InstantiatePrefab("Food", pos, rot, true);
            food.transform.position = pos;
            food.transform.rotation = rot;
            snakeBody.RemoveAt(i);
        }
        

    }

    #endregion
    public void AddBodyParts()
    {
        //CreateBodyParts();
        _level++;
        _isEatFood++;
        OnEatFoodEvent?.Invoke();
    }

    public Transform GetSnakehead()
    {
        return snakeBody[0].transform;
    }

    public int GetSnakeLevel() => _level;
}
