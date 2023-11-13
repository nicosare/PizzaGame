using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Task : MonoBehaviour
{
    private PlayerMove playerMove;
    protected ActionObject actionObject;
    protected InventoryObject inventoryObject;
    protected int amountOfObjects;


    private void Awake()
    {
        playerMove = FindObjectOfType<PlayerMove>();
    }

    protected void CallPlayer(Vector3 position)
    {
        playerMove.MoveTo(position);
        StartCoroutine(WaitPlayerCome());
    }

    private IEnumerator WaitPlayerCome()
    {
        yield return new WaitUntil(() => playerMove.IsCome);
        InnerDo();
        CompleteTask();
    }

    public void Do(Vector3 targetPosition)
    {
        CallPlayer(targetPosition);
    }

    public void Do(ActionObject actionObject, InventoryObject inventoryObject, int amountOfObjects = 1)
    {
        CallPlayer(actionObject.transform.position);
        this.actionObject = actionObject;
        this.inventoryObject = inventoryObject;
        this.amountOfObjects = amountOfObjects;
    }

    protected virtual void CompleteTask()
    {
        Destroy(gameObject);
    }

    public virtual void DestroyTask()
    {
        Destroy(gameObject);
    }

    protected virtual void InnerDo()
    {
        Debug.Log("InnerDo!");
        return;
    }
}
