public class TaskGive : Task
{
    protected override void InnerDo()
    {
        actionObject.Give(inventoryObject, amountOfObjects);
        actionObject.StartAction();
    }
    public override void DestroyTask()
    {
        actionObject.CancelAction();
        base.DestroyTask();
    }
}
