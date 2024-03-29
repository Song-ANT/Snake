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
    private SnakeData snakeData;
    private LvPanelUI lvUI;

    private int _level = 1;
    private int _isEatFood = 0;
    private float _countUp = 0f;

    public event Action OnEatFoodEvent;

    private bool _isPlayer;
    private Material _color;

    private void Awake()
    {
        _isPlayer = transform.CompareTag(Define.ObjectName.player);
        _color = _isPlayer ? Main.Resource.Load<Material>(Define.ObjectName.playerMaterial) :
            Main.Resource.Load<Material>(Define.ObjectName.enemyMaterial);
        //snakeData = Main.Snake.AddSnakeData(_level);
        snakeData = _isPlayer ? Main.Snake.AddSnakeData(_level, Define.ObjectName.player) : Main.Snake.AddSnakeData(_level);
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
            GameObject temp1 = Main.Resource.InstantiatePrefab(Define.PrefabName.snakeHeadPrefab, transform);
            temp1.GetComponent<MeshRenderer>().material = _color;
            temp1.tag = Define.ObjectName.head;

            lvUI = Main.UI.SetSubItemUI<LvPanelUI>(temp1.transform);
            lvUI.SetLvText(_level.ToString());
            lvUI.transform.position += Vector3.up;
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
            GameObject temp = Main.Resource.InstantiatePrefab(Define.PrefabName.snakeBodyPrefab, transform);
            temp.GetComponent<MeshRenderer>().material = _color;
            snakeBody.Add(temp);
            temp.GetComponent<SnakeMarker>().ClearMarkerList();
            temp.GetComponent<Snake>().SetSnakeBodyIndex(snakeBody.Count-1);
            _countUp = 0;
            _isEatFood -= 1;
        }


    }


    #region BeEatedMethod
    public void BeEatedHead()
    {
        for(int i = snakeBody.Count - 1; i >= 0; i--)
        {
            Destroy(snakeBody[i]);
            BodyToFood(i);
        }
        snakeBody.Clear();
        Destroy(gameObject);
    }


    public void BeEatedBody(int index)
    {
        for (int i = snakeBody.Count - 1; i >= index; i--)
        {
            Destroy(snakeBody[i]);
            BodyToFood(i);

            snakeBody.RemoveAt(i);
        }
    }


    private void BodyToFood(int i)
    {
        var pos = snakeBody[i].transform.position;
        var rot = snakeBody[i].transform.rotation;
        GameObject food = Main.Resource.InstantiatePrefab(Define.ObjectName.food, pos, rot, true);
        food.transform.position = pos;
        food.transform.rotation = rot;
    }

    #endregion
    public void AddBodyParts()
    {
        _level++;
        _isEatFood++;
        snakeData.level = _level;
        lvUI.SetLvText(_level.ToString());
        OnEatFoodEvent?.Invoke();
    }

    public Transform GetSnakehead()
    {
        return snakeBody[0].transform;
    }

    public int GetSnakeLevel() => _level;
}
