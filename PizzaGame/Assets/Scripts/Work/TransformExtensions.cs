using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class TransformExtensions
{
    public static List<Transform> GetChilds(this Transform parent)
    {
        var list = new List<Transform>();
        foreach (Transform child in parent)
            list.Add(child);
        return list;
    }
}
