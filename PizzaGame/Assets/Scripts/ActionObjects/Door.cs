using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : ActionObject
{
    [SerializeField] private Sprite openDoorIcon;
    [SerializeField] private Animator animator;
    private ActionButtonCanvas actionButton;
    public override Type typeOfNeededItem => throw new NotImplementedException();

    private void Start()
    {
        StartCoroutine(WaitStartDay());
        OpenButton(spawnPosition, openDoorIcon);
        actionButton = GetComponentInChildren<ActionButtonCanvas>();
    }

    public override void CancelAction()
    {
        OpenButton(spawnPosition, openDoorIcon);
    }

    public override void Interact()
    {
        TaskManager.Instance.CreateTask(TaskAction, this, null);
    }

    public override void ItemDownCast()
    {
        throw new NotImplementedException();
    }

    private IEnumerator WaitStartDay()
    {
        yield return new WaitUntil(() => CustomersManager.Instance.IsDayStarted);
        actionButton.CloseButton();
        TaskManager.Instance.CreateTask(TaskAction, this, null, 0, true);
    }

    public override void StartAction()
    {
        StopAllCoroutines();
        CustomersManager.Instance.IsDayStarted = true;
        animator.SetTrigger("OpenDoor");
    }
}
