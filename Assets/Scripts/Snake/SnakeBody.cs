using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : Snake
{

    public override void Eated()
    {
        base.Eated();
        Debug.Log($"{_snakeBodyIndex} : ∏ˆ≈ÎIndex");
        _controller.BeEatedBody(_snakeBodyIndex);
    }
}
