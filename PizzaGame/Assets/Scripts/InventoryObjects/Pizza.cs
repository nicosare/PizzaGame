using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Pizza", menuName = "Pizza", order = 51)]
public class Pizza : CookedInventoryObject
{
    public float MinimalCafeRating;
}