using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : Snake
{

    public override void Eated()
    {
        base.Eated();
        _controller.BeEatedBody(_snakeBodyIndex);
    }
}
