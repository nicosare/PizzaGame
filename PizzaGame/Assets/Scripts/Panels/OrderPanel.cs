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
    [SerializeField] private TextMeshProUGUI customerName;
    [SerializeField] private IngredientPanel ingredientPanel;
    [SerializeField] private Button button;
    private Order order;

    public void LoadData(Order order)
    {
        this.order = order;
        pizzaIcon.sprite = order.pizza.Icon;
        pizzaName.text = order.pizza.nameOfObject;
        customerName.text = order.customer.CustomerName.text;
        foreach (var ingredient in order.pizza.ingredients)
        {
            var newIngredientPanel = Instantiate(ingredientPanel, transform);
            newIngredientPanel.LoadData(ingredient);
        }
        if (order.pizza.ingredients.Any(ingredient => Inventory.Instance.GetAmountOfObject(ingredient) == 0))
            button.enabled = false;
        else
            button.enabled = true;
    }

    public void TakeOrder()
    {
        var window = transform.parent.parent.GetComponent<OrdersWindow>();
        if (OrderController.Instance.ActiveOrder == null)
        {
            OrderController.Instance.ActiveOrder = order;
            OrderController.Instance.Orders.Remove(order);
            window.ActionObjectCallBack.CookedInventoryObject = order.pizza;
            Debug.Log(order.ToString());
            TaskManager.Instance.CreateTask(window.ActionObjectCallBack.TaskCook, window.ActionObjectCallBack, order.pizza, 1, true);
            WindowsController.Instance.CloseWindow(window);
        }
        else
        {
            window.ActionObjectCallBack.CancelAction();
            WindowsController.Instance.CloseWindow(window);
        }

    }

    public void DestroyOrder()
    {
        var window = transform.parent.parent.GetComponent<OrdersWindow>();
        order.customer.Interact();
        window.ActionObjectCallBack.CancelAction();
        WindowsController.Instance.CloseWindow(window);
    }
}
