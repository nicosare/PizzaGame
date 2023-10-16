using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Seed", menuName = "Seed", order = 53)]
public class Seed : InventoryObject
{
    public Ingredient Ingredient;
    private int ingredientsAmount;
    public int TimeToGrow;
    [SerializeField] private int minAmountInclusive;
    [SerializeField] private int maxAmountExclusive;

    public int AmountOfIngredients()
    {
        return Random.Range(minAmountInclusive, maxAmountExclusive);
    }
}