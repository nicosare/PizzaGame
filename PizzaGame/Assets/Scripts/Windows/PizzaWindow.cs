using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaWindow : Window
{
    [SerializeField] private PizzaPanel pizzaPanel;

    private void LoadItems()
    {
        foreach (var pizza in ActionObjectCallBack.Pizzas)
        {
            var newPanel = Instantiate(pizzaPanel, windowField.transform);
            newPanel.LoadData(pizza);
        }
    }

    public override void StartAction(ActionObject actionObject)
    {
        ActionObjectCallBack = actionObject;
        foreach (Transform item in windowField.transform)
            Destroy(item.gameObject);

        LoadItems();
    }

    public override void UpdateWindow()
    {
        if (windowField.activeSelf)
        {
            if (ActionObjectCallBack != null)
                StartAction(ActionObjectCallBack);
        }
    }

    public void TakeOrder()
    {
        ActionObjectCallBack.StartAction();
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
