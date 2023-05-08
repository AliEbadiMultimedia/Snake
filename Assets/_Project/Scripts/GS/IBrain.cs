public interface IBrain
{

    void MakeDecisionWithDelay(Snake snake);
    void MakeDecisionInstant(Snake snake);
    void SetSnakeDirection(Snake snake);
}

