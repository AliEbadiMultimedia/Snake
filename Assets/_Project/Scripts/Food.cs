using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;


    void Start()
    {
        ResetPosition();


        //gridArea.bounds.min.x;
        //gridArea.bounds.min.y;
        //gridArea.bounds.max.y;
        //gridArea.bounds.max.x;
    }

    private void ResetPosition()
    {
        Vector2 pos;
        do
        {
            pos.x = (int) Random.Range(gridArea.bounds.min.x, gridArea.bounds.max.x);
            pos.y = (int) Random.Range(gridArea.bounds.min.y, gridArea.bounds.max.y);
        } while (IsOnSnakeBody(pos));

        gameObject.transform.position = pos;
    }

    bool IsOnSnakeBody(Vector2 pos)
    {
        return GameController.instance.snake.segments.Exists(x => x.transform.position.Equals(pos));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var snake = other.GetComponent<Snake>();
        if (snake != null)
        {
            ResetPosition();
        }
    }
}