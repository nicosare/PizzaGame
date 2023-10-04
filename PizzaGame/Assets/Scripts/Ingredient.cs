using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Ingredient", order = 52)]
public class Ingredient : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private string nameOfIngredient;
}