using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public static class TextAssetExtensions
{
    public static string GetRandomName(this TextAsset text)
    {
        return text.text.Split(", ").ToList().Shuffle().First();
    }
}
