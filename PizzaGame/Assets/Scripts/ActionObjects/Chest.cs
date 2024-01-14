using System;
using UnityEngine;

public class Chest : ActionObject
{
    [SerializeField] private Sprite takeIcon;

    public override Type typeOfNeededItem => throw new NotImplementedException();

    private void Awake()
    {
        Item = ChestsManager.Instance.ChooseItem();
    }

    private void Start()
    {
        OpenButton(spawnPosition, takeIcon);
    }

    public override void CancelAction()
    {
        OpenButton(spawnPosition, takeIcon);
    }

    public override void Interact()
    {
        TaskManager.Instance.CreateTask(TaskGive, this, Item, 
            UnityEngine.Random.Range(ChestsManager.Instance.MinAmountItems, ChestsManager.Instance.MaxAmountItems));
    }

    public override void ItemDownCast()
    {
        throw new NotImplementedException();
    }

    public override void StartAction()
    {
        return;
    }
}
