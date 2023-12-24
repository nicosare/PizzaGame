using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OrderPanel : MonoBehaviour
{
    [SerializeField] private Image pizzaIcon;
    [SerializeField] private TextMeshProUGUI pizzaName;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private IngredientPanel ingredientPanel;
    [SerializeField] private Button button;
    [SerializeField] private Transform ingredientField;
    [SerializeField] private Transform starsField;
    [SerializeField] private TextMeshProUGUI ratingText;
    private Order order;

    public void LoadData(Order order)
    {
        this.order = order;
        pizzaIcon.sprite = order.Pizza.Icon;
        pizzaName.text = order.Pizza.nameOfObject;
        cost.text = order.Cost.ToString();
        ratingText.text = $"+{order.Rating}";
        SetStars();
        foreach (var ingredient in order.Pizza.ingredients)
        {
            var newIngredientPanel = Instantiate(ingredientPanel, ingredientField);
            newIngredientPanel.LoadData(ingredient);
        }
        if (order.Pizza.ingredients.Any(ingredient => Inventory.Instance.GetAmountOfObject(ingredient) == 0))
            button.enabled = false;
        else
            button.enabled = true;
    }

    private void SetStars()
    {
        for (var i = 0; i < order.PizzaLevel; i++)
            starsField.GetChild(i).gameObject.SetActive(true);
    }

    public void TakeOrder()
    {
        var window = transform.GetComponentInParent<OrdersWindow>();
        if (!TaskManager.Instance.BlockCreateTask && window.ActionObjectCallBack.GetComponent<PizzaTable>().GetFreeFurnaces().Count > 0)
        {
            if (OrderController.Instance.FirstActiveOrder == null)
            {
                OrderController.Instance.FirstActiveOrder = order;
                OrderController.Instance.Orders.Remove(order);
                window.ActionObjectCallBack.CookedInventoryObject = order.Pizza;
                TaskManager.Instance.CreateTask(window.ActionObjectCallBack.TaskCook, window.ActionObjectCallBack, order.Pizza, 1, true);
                WindowsController.Instance.CloseWindow(window);
            }
            else if (OrderController.Instance.SecondActiveOrder == null)
            {
                OrderController.Instance.SecondActiveOrder = order;
                OrderController.Instance.Orders.Remove(order);
                window.ActionObjectCallBack.CookedInventoryObject = order.Pizza;
                TaskManager.Instance.CreateTask(window.ActionObjectCallBack.TaskCook, window.ActionObjectCallBack, order.Pizza, 1, true);
                WindowsController.Instance.CloseWindow(window);

            }
        }
        else
        {
            window.ActionObjectCallBack.CancelAction();
            WindowsController.Instance.CloseWindow(window);
        }

    }

    public void DestroyOrder()
    {
        var window = transform.GetComponentInParent<OrdersWindow>();
        order.Customer.Interact();
        window.ActionObjectCallBack.CancelAction();
        WindowsController.Instance.CloseWindow(window);
    }
}
