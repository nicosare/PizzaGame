using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pizza", menuName = "Pizza", order = 51)]
public class Pizza : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private string nameOfPizza;
    [SerializeField] private List<Ingredient> ingredients;
    [SerializeField] private int cost;
}