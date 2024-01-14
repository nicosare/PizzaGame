using System;
using UnityEngine;

public class NightDoor : ActionObject
{
    [SerializeField] private Sprite openDoorIcon;
    [SerializeField] private Animator animator;
    private ActionButtonCanvas actionButton;
    public override Type typeOfNeededItem => throw new NotImplementedException();

    private void Start()
    {
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

    public override void StartAction()
    {
        animator.SetTrigger("OpenDoor");
        GameManager.Instance.NightToDay();
    }
}
