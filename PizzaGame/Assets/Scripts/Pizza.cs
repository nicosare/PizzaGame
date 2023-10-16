using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Pizza", menuName = "Pizza", order = 51)]
public class Pizza : InventoryObject
{
    [SerializeField] private List<Ingredient> ingredients;
    [SerializeField] private int cost;
    public float minimalCafeRating;
}