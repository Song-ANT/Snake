using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Head"))
        {
            SnakeController snake = other.transform.root.GetComponent<SnakeController>();
            snake.AddBodyParts();

            Main.Pool.Push(gameObject);
        }
    }
}
