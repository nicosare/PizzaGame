using System.Linq;
using UnityEngine;

public static class TextAssetExtensions
{
    public static string GetRandomName(this TextAsset text)
    {
        return text.text.Split(", ").ToList().Shuffle().First();
    }
}
