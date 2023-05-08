using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GridArea GridArea;
    public Snake snake;
    public Food food;

    public float GameInterval = 0.1f;
    public bool autoPlay = false;
    private IBrain brain;
    public float waitForColor = 0.1f;

    void Awake()
    {
        instance = this;
        brain = snake.GetComponent<IBrain>();
    }
    void Start()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        while (true)
        {
            if (autoPlay)
            {
                SearchInstant();
                yield return new WaitForSeconds(GameInterval);
                SetSnakeDirection();
                PlayOneMove();
            }
            else
            {
                yield return new WaitForSeconds(GameInterval);
            }
        }
    }

    public void Search()
    {
        brain.MakeDecisionWithDelay(snake);
    }
    public void SearchInstant()
    {
        brain.MakeDecisionInstant(snake);
    }
   public void PlayOneMove()
    {
        snake.MoveSnake();
    }

   public void SetSnakeDirection()
   {
      brain.SetSnakeDirection(snake);
   }
   
   public void SetSnakeDirectionAndMove()
   {
       SetSnakeDirection();
       PlayOneMove();
   }
}