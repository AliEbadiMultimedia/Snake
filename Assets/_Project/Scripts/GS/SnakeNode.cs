using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SnakeNode
{
    public SnakeNode parent;
    public Vector3 headPos;
    public List<Vector3> segments;
    public Vector3 direction;

    public SnakeNode(SnakeNode parent, Vector3 headPos, List<Vector3> segments, Vector3 direction)
    {
        this.parent = parent;
        this.headPos = headPos;
        this.segments = segments;
        this.direction = direction;
    }

    public SnakeNode(SnakeNode parent, List<Vector3> segments, Vector3 moveDirection)
    {
        this.parent = parent;
        this.direction = moveDirection;
        this.segments = new List<Vector3>(segments);
        for (int i = this.segments.Count - 1; i > 0; i--)
        {
            this.segments[i] = this.segments[i - 1];
        }

        this.segments[0] = new Vector2(this.segments[0].x + direction.x, this.segments[0].y + direction.y);

        this.headPos = this.segments[0];
    }

    public bool IsGoal()
    {
        if (headPos.Equals(GameController.instance.food.transform.position))
        {
            return true;
        }

        return false;
    }

    public List<SnakeNode> Expand()
    {
        List<SnakeNode> children = new List<SnakeNode>();

        if (direction == Vector3.right)
        {
            var right = new SnakeNode(this, this.segments, Vector3.right);
            if (right.IsValidMove())
            {
                children.Add(right);
            }
            var down = new SnakeNode(this, this.segments, Vector3.down);
            if (down.IsValidMove())
            {
                children.Add(down);
            }
            
            var up = new SnakeNode(this, this.segments, Vector3.up);
            if (up.IsValidMove())
            {
                children.Add(up);
            }

            
            
        }

        if (direction == Vector3.left)
        {
            var up = new SnakeNode(this, this.segments, Vector3.up);
            if (up.IsValidMove())
            {
                children.Add(up);
            }

            var down = new SnakeNode(this, this.segments, Vector3.down);
            if (down.IsValidMove())
            {
                children.Add(down);
            }

            var left = new SnakeNode(this, this.segments, Vector3.left);
            if (left.IsValidMove())
            {
                children.Add(left);
            }
        }

        if (direction == Vector3.up)
        {
            var left = new SnakeNode(this, this.segments, Vector3.left);
            if (left.IsValidMove())
            {
                children.Add(left);
            }

            var right = new SnakeNode(this, this.segments, Vector3.right);
            if (right.IsValidMove())
            {
                children.Add(right);
            }
            
            var up = new SnakeNode(this, this.segments, Vector3.up);
            if (up.IsValidMove())
            {
                children.Add(up);
            }
        }

        if (direction == Vector3.down)
        {
            var left = new SnakeNode(this, this.segments, Vector3.left);
            if (left.IsValidMove())
            {
                children.Add(left);
            }

            var right = new SnakeNode(this, this.segments, Vector3.right);
            if (right.IsValidMove())
            {
                children.Add(right);
            }
            
            var down = new SnakeNode(this, this.segments, Vector3.down);
            if (down.IsValidMove())
            {
                children.Add(down);
            }
        }

        return children;
    }

    private bool IsValidMove()
    {
        var bound = GameController.instance.GridArea.BoxCollider2D.bounds;
        var minX = bound.min.x;
        var minY = bound.min.y;
        var maxX = bound.max.x;
        var maxY = bound.max.y;

        if (headPos.x<minX)
        {
            return false;
        }
        if (headPos.x>maxX)
        {
            return false;
        }
        if (headPos.y<minY)
        {
            return false;
        }
        if (headPos.y>maxY)
        {
            return false;
        }

        
        for (int i = 1; i < segments.Count; i++)
        {
            if (segments[i].Equals(headPos))
            {
                return false;
            }
        }

 


        return true;
    }

    public bool IsEqual(SnakeNode next)
    {
        if (!next.headPos.Equals(this.headPos))
        {
            return false;
        }
/*
        if (!next.direction.Equals(this.direction))
        {
            return false;
        }

        if (next.segments.Count != this.segments.Count)
        {
            return false;
        }

        for (int i = 0; i < next.segments.Count; i++)
        {
            if (!next.segments[i].Equals(this.segments[i]))
            {
                return false;
            }
        }
*/
        return true;
    }
}