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

        //Debug.Log($"otherName : {otherRoot.name}, myName : {myRoot.name}, " +
        //    $"\n other : {otherRoot.GetInstanceID()}, my : {myRoot.GetInstanceID()}");
        
        if (otherRoot.GetInstanceID() == myRoot.GetInstanceID()) return;

        if (other.CompareTag("Food"))
        {
            Debug.Log($"¹äÀÌ¶û ¸¸³²");
            other.GetComponent<Food>().Eated();
            _controller.AddBodyParts();
        }

        if (other.gameObject.CompareTag("Head") || other.gameObject.CompareTag("Body"))
        {
            Debug.Log($"¸Ó¸®³ª ¸öÅëÀÌ¶û ºÎµúÈû");
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
        Debug.Log($"{_snakeBodyIndex} : ¸Ó¸®Index");
        _controller.BeEatedHead();
    }

}
