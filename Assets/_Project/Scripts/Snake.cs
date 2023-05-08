using System;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Mathematics;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public GameObject segmentPrefab;
    public List<Transform> segments;
    public Vector2 direction=Vector2.right;

    void Awake()
    {
        segments.Add(this.transform);
    }
    
    public void MoveSnake()
    {
        for (int i = segments.Count-1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        segments[segments.Count - 1].GetComponent<BoxCollider2D>().enabled = true;
        segments[0].position=new Vector2(segments[0].position.x+direction.x,segments[0].position.y+direction.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var food = other.GetComponent<Food>();
        if (food!=null)
        {
            Grow();
        }
        else
        {
            ResetState();
        }
    }

    private void ResetState()
    {
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(this.transform);
        transform.position=Vector3.zero;
    }

    private void Grow()
    {
        var pos= segments[segments.Count - 1].position;
        var segment = Instantiate(segmentPrefab, pos, Quaternion.identity);
        segments.Add(segment.transform);
        segment.GetComponent<BoxCollider2D>().enabled = false;
        // MoveSnake();
    }
}