using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    public static List<T> Shuffle<T>(this List<T> list)
    {
        for (var i = 0; i < list.Count; i++)
        {
            var randomIndex = Random.Range(0, list.Count);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
        return list;
    }
}