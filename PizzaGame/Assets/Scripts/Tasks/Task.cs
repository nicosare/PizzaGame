using System.Collections;
using UnityEngine;

public class Task : MonoBehaviour
{
    private Moving playerMove;
    protected ActionObject actionObject;
    protected InventoryObject inventoryObject;
    protected int amountOfObjects;
    
    private void Awake()
    {
        playerMove = transform.parent.GetComponent<TaskManager>().Player;
        playerMove.gameObject.SetActive(true);
    }

    protected void CallPlayer(Vector3 position)
    {
        playerMove.MoveTo(position);
        StartCoroutine(WaitPlayerCome());
    }

    private IEnumerator WaitPlayerCome()
    {
        yield return new WaitUntil(() => playerMove.IsCome);
        TaskManager.Instance.BlockCreateTask = true;

        if (actionObject != null && actionObject.Particles != null)
        {
            var particles = Instantiate(actionObject.Particles, (playerMove.transform.position + actionObject.transform.position) / 2, Quaternion.identity.normalized, actionObject.transform);
            yield return new WaitForSeconds(actionObject.WaitingTimeBeforeAction);
            Destroy(particles.gameObject);
        }

        TaskManager.Instance.BlockCreateTask = false;
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
        return;
    }
}
