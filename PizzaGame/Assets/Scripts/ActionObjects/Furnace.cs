using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Furnace : ActionObject
{
    [SerializeField] private float cookingTime;
    [SerializeField] private TimerCircle timer;
    [SerializeField] private Sprite takePizzaIcon;
    public override Type typeOfNeededItem => throw new NotImplementedException();

    private void Awake()
    {
        timer.MaxTime = cookingTime;
        timer.gameObject.SetActive(false);
    }

    public override void CancelAction()
    {
        if (Item != null)
            Item = CookedInventoryObject = null;
        else if (CookedInventoryObject != null)
            OpenButton(spawnPosition, takePizzaIcon);
    }

    public override void Interact()
    {
        TaskManager.Instance.CreateTask(TaskGive, this, CookedInventoryObject, 1, true);
    }

    public override void ItemDownCast()
    {
        throw new NotImplementedException();
    }

    public override void StartAction()
    {
        if (CookedInventoryObject == null)
        {
            Debug.Log(OrderController.Instance.ActiveOrder);
            CookedInventoryObject = OrderController.Instance.ActiveOrder.pizza;
            timer.gameObject.SetActive(true);
        }
        else
        {
            TaskManager.Instance.CreateTask(TaskTake, OrderController.Instance.ActiveOrder.customer, CookedInventoryObject, 1, true);
        }
    }

    public override void DoAfterTimer()
    {
        timer.gameObject.SetActive(false);
        OpenButton(spawnPosition, takePizzaIcon);
    }
}
