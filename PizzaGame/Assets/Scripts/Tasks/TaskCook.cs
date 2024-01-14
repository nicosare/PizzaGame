using System.Collections;
using UnityEngine;

public class TaskCook : Task
{
    protected override void InnerDo()
    {
        var cookedInventoryObject = (CookedInventoryObject)inventoryObject;
        foreach (var item in cookedInventoryObject.ingredients)
            if (Inventory.Instance.CheckEnoughAmountObjects(item, amountOfObjects))
            {
                actionObject.Take(item, amountOfObjects);
            }
        actionObject.Give(cookedInventoryObject, amountOfObjects);
        actionObject.StartAction();
    }
    public override void DestroyTask()
    {
        actionObject.CancelAction();
        base.DestroyTask();
    }

    private IEnumerator Cooking()
    {
        yield return new WaitForSeconds(2);
        var cookedInventoryObject = (CookedInventoryObject)inventoryObject;
        foreach (var item in cookedInventoryObject.ingredients)
            actionObject.Take(item, amountOfObjects);
        actionObject.Give(cookedInventoryObject, amountOfObjects);
    }
}
