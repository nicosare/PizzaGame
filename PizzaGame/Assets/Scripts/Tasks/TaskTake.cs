public class TaskTake : Task
{
    protected override void InnerDo()
    {
        if (Inventory.Instance.CheckEnoughAmountObjects(inventoryObject, amountOfObjects))
        {
            actionObject.Take(inventoryObject, amountOfObjects);
            actionObject.SetItem(inventoryObject);
            actionObject.StartAction();
        }
        else
            DestroyTask();
    }

    public override void DestroyTask()
    {
        actionObject.CancelAction();
        base.DestroyTask();
    }
}
