using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;
    private Task task;

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

    public void CreateTask(Task task, Vector3 targetPosition)
    {
        ResetTask();
        this.task = Instantiate(task, transform);
        this.task.Do(targetPosition);
    }

    public void CreateTask(Task task, ActionObject actionObject, InventoryObject inventoryObject, int amount = 1)
    {
        ResetTask();
        this.task = Instantiate(task, transform);
        this.task.Do(actionObject, inventoryObject, amount);
    }

    public void ResetTask()
    {
        if (transform.childCount > 0)
            task.DestroyTask();
    }
}
