using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Seed", menuName = "Seed", order = 53)]
public class Seed : InventoryObject
{
    public Ingredient Ingredient;
    public float TimeToGrow;
    [SerializeField] private int minAmountInclusive;
    [SerializeField] private int maxAmountExclusive;
    public MeshFilter[] MeshFilters;

    public int AmountOfIngredients()
    {
        return Random.Range(minAmountInclusive, maxAmountExclusive);
    }
}