public class TaskAction : Task
{
    protected override void InnerDo()
    {
        actionObject.StartAction();
    }

    public override void DestroyTask()
    {
        actionObject.CancelAction();
        base.DestroyTask();
    }
}
