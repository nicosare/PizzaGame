using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PizzaTable : ActionObject
{
    [SerializeField] private Sprite createPizzaIcon;
    [SerializeField] private Furnace furnace;
    public override Type typeOfNeededItem => throw new NotImplementedException();

    private void Awake()
    {
        OpenButton(spawnPosition, createPizzaIcon);
    }

    public override void CancelAction()
    {
        Pizzas.Clear();
        OpenButton(spawnPosition, createPizzaIcon);
    }

    public override void Interact()
    {
        foreach (var order in OrderController.Instance.Orders)
            Pizzas.Add(order.pizza);
        WindowsController.Instance.OpenWindow(window, this);
    }

    public override void ItemDownCast()
    {
        throw new NotImplementedException();
    }

    public override void StartAction()
    {
        Pizzas.Remove((Pizza)CookedInventoryObject);
        TaskManager.Instance.CreateTask(TaskTake, furnace, CookedInventoryObject, 1, true);
    }
}
