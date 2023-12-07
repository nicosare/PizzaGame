using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameOfIngredient;
    [SerializeField] private TextMeshProUGUI needAmountText;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private List<Image> panels;
    public void LoadData(Ingredient ingredient)
    {
        nameOfIngredient.text = ingredient.nameOfObject;
        var amount = Inventory.Instance.GetAmountOfObject(ingredient);
        amountText.text = amount.ToString();
        if (amount > 0)
            foreach (var panel in panels)
                panel.sprite = sprites[0];
        else
            foreach (var panel in panels)
                panel.sprite = sprites[1];
    }
}
