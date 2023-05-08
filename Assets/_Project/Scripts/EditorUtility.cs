using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorUtility 
{
    
    [MenuItem("EditorUtility/Instantiate Selected")]
    static void InstantiateTileInGridArea()
    {
         var gridArea= GameObject.Find("GridArea");
         var bound = gridArea.GetComponent<BoxCollider2D>().bounds;
         
         for (int j =(int) bound.min.y; j <= bound.max.y; j++)
         {
             for (int i =(int) bound.min.x; i <= bound.max.x; i++)
             {
                var obj = PrefabUtility.InstantiatePrefab(Selection.activeObject as GameObject,gridArea.transform);
                (obj as GameObject).transform.position=new Vector3(i,j);
             }
         }

    }

}
