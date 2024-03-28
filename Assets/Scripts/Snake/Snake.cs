using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Snake : MonoBehaviour, IEated
{
    protected SnakeController _controller;
    protected int _snakeLevel;
    protected int _snakeBodyIndex;

    protected void Awake()
    {
        _controller = transform.root.GetComponent<SnakeController>();
        _snakeLevel = _controller.GetSnakeLevel();
        _controller.OnEatFoodEvent -= GetSnakeLevel;
        _controller.OnEatFoodEvent += GetSnakeLevel;
    }

    private void OnDestroy()
    {
        _controller.OnEatFoodEvent -= GetSnakeLevel;
    }

    public virtual void Eated()
    {
        
    }

    public void SetSnakeBodyIndex(int index)
    {
        _snakeBodyIndex = index;
    }
    private void GetSnakeLevel()
    {
        _snakeLevel = _controller.GetSnakeLevel();
    }
}
