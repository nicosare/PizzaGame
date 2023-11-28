using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PizzaPanel : MonoBehaviour
{
    [SerializeField] private Image pizzaIcon;
    [SerializeField] private TextMeshProUGUI pizzaName;
    [SerializeField] private IngredientPanel ingredientPanel;
    private Pizza pizza;
    private Button button;
    private Order order;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void LoadData(Pizza pizza)
    {
        this.pizza = pizza;
        pizzaIcon.sprite = pizza.Icon;
        pizzaName.text = pizza.nameOfObject;

        foreach (var ingredient in pizza.ingredients)
        {
            var newIngredientPanel = Instantiate(ingredientPanel, transform);
            newIngredientPanel.LoadData(ingredient);
        }
    }

    public void TakeOrder()
    {
        var window = transform.parent.parent.GetComponent<PizzaWindow>();

        window.TakeOrder();
        order = new Order(pizza, window.ActionObjectCallBack as Customer);
        OrderController.Instance.Orders.Add(order);

        WindowsController.Instance.CloseWindow(window);
    }
}
