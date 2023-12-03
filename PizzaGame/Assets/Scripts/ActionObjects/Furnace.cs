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
    public float TimeScaleToCook = 1;
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
        if (!TaskManager.Instance.BlockCreateTask)
            TaskManager.Instance.CreateTask(TaskGive, this, CookedInventoryObject, 1, true);
        else
            OpenButton(spawnPosition, takePizzaIcon);
    }

    public override void ItemDownCast()
    {
        throw new NotImplementedException();
    }

    public override void StartAction()
    {
        if (CookedInventoryObject == null)
        {
            if (OrderController.Instance.FirstActiveOrder != null)
            {
                if (OrderController.Instance.SecondActiveOrder == null)
                {
                    Debug.Log(OrderController.Instance.FirstActiveOrder);
                    CookedInventoryObject = OrderController.Instance.FirstActiveOrder.pizza;
                }
                else
                    CookedInventoryObject = OrderController.Instance.SecondActiveOrder.pizza;
            }
            SetTimerTime();
            timer.gameObject.SetActive(true);
        }
        else
        {
            if (OrderController.Instance.FirstActiveOrder != null)
            {
                if (OrderController.Instance.SecondActiveOrder == null)
                    TaskManager.Instance.CreateTask(TaskTake, OrderController.Instance.FirstActiveOrder.customer, CookedInventoryObject, 1, true);
                else
                    TaskManager.Instance.CreateTask(TaskTake, OrderController.Instance.SecondActiveOrder.customer, CookedInventoryObject, 1, true);
            }
        }
    }

    public override void DoAfterTimer()
    {
        timer.gameObject.SetActive(false);
        OpenButton(spawnPosition, takePizzaIcon);
    }

    public void SetTimerTime()
    {
        timer.MaxTime = cookingTime / TimeScaleToCook;
    }
}
