using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Snake snake;

    void Awake()
    {
        snake = GetComponent<Snake>();
    }

    private void Update()
    {
        if (snake.direction.x != 0)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                snake.direction = Vector2.up;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                snake.direction = Vector2.down;
            }
        }

        if (snake.direction.y != 0)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                snake.direction = Vector2.left;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                snake.direction = Vector2.right;
            }
        }
    }
}