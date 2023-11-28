using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskAction : Task
{
    protected override void InnerDo()
    {
        Debug.Log(actionObject);
        actionObject.StartAction();
    }

    public override void DestroyTask()
    {
        actionObject.CancelAction();
        base.DestroyTask();
    }
}
