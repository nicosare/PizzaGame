using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PizzaTable : ActionObject
{
    [SerializeField] private Sprite createPizzaIcon;
    [SerializeField] private List<Furnace> furnaces;
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
        var freeFurnace = furnaces.Where(furnace => furnace.CookedInventoryObject == null && furnace.gameObject.activeSelf).First();
        TaskManager.Instance.CreateTask(TaskTake, freeFurnace, CookedInventoryObject, 1, true);
    }
}
