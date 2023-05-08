using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class DFSController : MonoBehaviour, IBrain
{
    public List<SnakeNode> path;
    [HideInInspector] public List<SnakeNode> closed;
    [HideInInspector] public Stack<SnakeNode> fringe;
    public int maximumSearchCount = 1;
    private SnakeNode start;

    void Awake()
    {
        path = new List<SnakeNode>();
        closed = new List<SnakeNode>();
        fringe = new Stack<SnakeNode>();
    }

    public void MakeDecisionWithDelay(Snake snake)
    {
        if (path.Count == 0)
        {
            StartCoroutine(DFS(snake));
        }
    }

    public void MakeDecisionInstant(Snake snake)
    {
        if (path.Count == 0)
        {
            DFSInstant(snake);
        }
    }

    public void SetSnakeDirection(Snake snake)
    {
        if (path.Count != 0)
        {
            snake.direction=path[0].direction;
            path.RemoveAt(0);
        }  
    }

    IEnumerator DFS(Snake snake)
    {
        GameController.instance.GridArea.ResetState();
        fringe.Clear();
        closed.Clear();
        start = new SnakeNode(null, snake.segments[0].transform.position, snake.segments.ConvertToVector3(),
            snake.direction);
        fringe.Push(start);

        int count = 0;
        while (count < maximumSearchCount)
        {
            count++;
            SnakeNode next = SelectNode();
            if (closed.Exists(x => x.IsEqual(next)))
            {
                continue;
            }

            // turn tile to red
            GameController.instance.GridArea.GetTile(next.headPos).ChangeColor(Color.yellow);
            yield return new WaitForSeconds(GameController.instance.waitForColor);
            if (next.IsGoal())
            {
                path= CreatePath(next);
                yield break;
            }

            closed.Add(next);
            Expand(next);
            //var children= next.Expand();
        }
    }
    void DFSInstant(Snake snake)
    {
        //GameController.instance.GridArea.ResetState();
        fringe.Clear();
        closed.Clear();
        start = new SnakeNode(null, snake.segments[0].transform.position, snake.segments.ConvertToVector3(),
            snake.direction);
        fringe.Push(start);

        int count = 0;
        while (count < maximumSearchCount)
        {
            count++;
            SnakeNode next = SelectNode();
            if (closed.Exists(x => x.IsEqual(next)))
            {
                continue;
            }

            // turn tile to red
            //GameController.instance.GridArea.GetTile(next.headPos).ChangeColor(Color.yellow);
            //yield return new WaitForSeconds(GameController.instance.waitForColor);
            if (next.IsGoal())
            {
                path= CreatePath(next);
                 break;
            }

            closed.Add(next);
            Expand(next);
            //var children= next.Expand();
        }
    }


    private List<SnakeNode> CreatePath(SnakeNode next)
    {
        List<SnakeNode> path=new List<SnakeNode>();

        path.Add(next);
        
        while (next.parent!=null)
        {
            path.Add(next.parent);
            next = next.parent;
        }
        path.RemoveAt(path.Count-1);

        path.Reverse();
        return path;
    }

    private void Expand(SnakeNode next)
    {
        var children = next.Expand();
        foreach (var child in children)
        {
            fringe.Push(child);
        }
    }

    private SnakeNode SelectNode()
    {
        return fringe.Pop();
    }
}