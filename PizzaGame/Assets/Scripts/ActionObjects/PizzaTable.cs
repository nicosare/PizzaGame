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
        if (OrderController.Instance.Orders.Count > 0)
        {
            foreach (var order in OrderController.Instance.Orders)
                Pizzas.Add(order.Pizza);
            WindowsController.Instance.OpenWindow(window, this);
        }
        else
        {
            CancelAction();
            Message.Instance.LoadMessage("Нет активных заказов!", 1);
        }

    }

    public override void ItemDownCast()
    {
        throw new NotImplementedException();
    }

    public override void StartAction()
    {
        Pizzas.Remove((Pizza)CookedInventoryObject);
        TaskManager.Instance.CreateTask(TaskTake, GetFreeFurnaces().First(), CookedInventoryObject, 1, true);
    }

    public List<Furnace> GetFreeFurnaces()
    {
        return furnaces.Where(furnace => furnace.CookedInventoryObject == null && furnace.gameObject.activeInHierarchy).ToList();
    }
}
