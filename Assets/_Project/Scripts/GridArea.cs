using System;
using System.Collections.Generic;
using UnityEngine;

public class GridArea : MonoBehaviour
{
    public BoxCollider2D BoxCollider2D;
    public List<Tile> tiles;

    private void OnValidate()
    {
        tiles=new List<Tile>();
        tiles.AddRange(GetComponentsInChildren<Tile>());
    }

    public Tile GetTile(Vector3 pos)
    {
        return tiles.Find(x => x.transform.position == pos);
    }
    public void ResetState()
    {
        foreach (var tile in tiles)
        {
            tile.sprite.color=Color.white;
            tile.count = 1;
        }
    }
}