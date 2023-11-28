using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientPanel : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameAndAmountOfIngredient;

    public void LoadData(Ingredient ingredient)
    {
        icon.sprite = ingredient.Icon;
        var amount = Inventory.Instance.GetAmountOfObject(ingredient);
        nameAndAmountOfIngredient.text = $"{ingredient.nameOfObject}";
        if (amount > 0)
            nameAndAmountOfIngredient.color = Color.green;
        else
            nameAndAmountOfIngredient.color = Color.red;
    }
}
