using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SnakeHead : Snake
{
    

    private void OnTriggerEnter(Collider other)
    {
        var otherRoot = other.transform.root;
        var myRoot = transform.root;

        
        if (otherRoot.GetInstanceID() == myRoot.GetInstanceID()) return;

        if (other.CompareTag(Define.ObjectName.food))
        {
            other.GetComponent<Food>().Eated();
            _controller.AddBodyParts();
        }

        if (other.gameObject.CompareTag(Define.ObjectName.head) || other.gameObject.CompareTag(Define.ObjectName.body))
        {
            SnakeController otherSnakeController = otherRoot.GetComponent<SnakeController>();
            int otherSnakeLevel = otherSnakeController.GetSnakeLevel();
            if (otherSnakeLevel < _snakeLevel)
            {
                other.GetComponent<Snake>().Eated();
            }

        }

    }


    public override void Eated()
    {
        base.Eated();
        _controller.BeEatedHead();

        if(_controller.IsPlayer) Main.UI.SetSceneUI<GameOverSceneUI>();
    }

}
