
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class UtilityExtension 
{
    public static List<Vector3> ConvertToVector3(this List<Transform> segments)
    {
        return segments.Select(x => x.transform.position).ToList();
    }
}