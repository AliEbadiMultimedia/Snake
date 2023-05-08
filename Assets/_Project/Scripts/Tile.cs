using System.Collections;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public SpriteRenderer sprite;
  public  int count=1;

    

    public void ChangeColor(Color c)
    {
        
        sprite.color=Color.Lerp(Color.white, c, count++ / 5f);
        //StartCoroutine(ChangeColorWithDelay(c));
    }

    
}