using System.Linq;
using UnityEngine;

public class OrdersWindow : Window
{
    [SerializeField] private OrderPanel orderPanel;
    [SerializeField] private PizzaPanel pizzaPanel;

    private void LoadItems()
    {
        foreach (var order in OrderController.Instance.Orders.OrderBy(order => order.PizzaLevel))
        {
            var newPanel = Instantiate(orderPanel, windowField.transform);
            newPanel.LoadData(order);
        }
    }

    private void LoadMenu()
    {
        var availablePizzas = Menu.Instance.AvailablePizzas.OrderBy(pizza => pizza.ingredients.Count);
        foreach (var pizza in availablePizzas)
        {
            var newPanel = Instantiate(pizzaPanel, windowField.transform);
            newPanel.LoadCleanData(pizza);
        }
    }

    public override void StartAction(ActionObject actionObject)
    {
        if (actionObject != null)
            ActionObjectCallBack = actionObject;
        foreach (Transform item in windowField.transform)
            Destroy(item.gameObject);
        if (pizzaPanel == null)
            LoadItems();
        else
            LoadMenu();

    }

    public override void UpdateWindow()
    {
        if (windowField.activeSelf)
        {
            StartAction(ActionObjectCallBack);
        }
    }

    public override void CloseWindow()
    {
        if (windowField.activeSelf)
        {
            foreach (Transform item in windowField.transform)
                Destroy(item.gameObject);
            gameObject.SetActive(false);
        }
        ActionObjectCallBack = null;
    }
}
