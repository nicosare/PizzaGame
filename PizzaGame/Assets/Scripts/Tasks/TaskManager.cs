using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;
    private Task task;
    private Guid actualTaskUID;
    public bool BlockCreateTask;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public Guid CreateTask(Task task, Vector3 targetPosition, bool isTaskIrrevocable = false)
    {
        if (!BlockCreateTask)
        {
            BlockCreateTask = isTaskIrrevocable;
            ResetTask();
            this.task = Instantiate(task, transform);
            this.task.Do(targetPosition);
        }
        return actualTaskUID = Guid.NewGuid();
    }

    public Guid CreateTask(Task task, ActionObject actionObject, InventoryObject inventoryObject, int amount = 1, bool isTaskIrrevocable = false)
    {
        if (!BlockCreateTask)
        {
            BlockCreateTask = isTaskIrrevocable;
            ResetTask();
            this.task = Instantiate(task, transform);
            Debug.Log(actionObject);
            this.task.Do(actionObject, inventoryObject, amount);
        }
        else
            CancelTasK(actionObject);
        return actualTaskUID = Guid.NewGuid();
    }

    private void CancelTasK(ActionObject actionObject)
    {
        actionObject.CancelAction();
    }

    public bool CheckActualTask(Guid taskUID)
    {
        return actualTaskUID == taskUID;
    }

    public void ResetTask()
    {
        if (transform.childCount > 0)
            task.DestroyTask();
    }
}
