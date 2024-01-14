using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionObject : MonoBehaviour, IInteractable
{
    [SerializeField] protected Window window;
    [SerializeField] protected ActionButtonCanvas actionButtonCanvas;
    [SerializeField] protected Vector3 spawnPosition;
    [SerializeField] protected float iconScale;
    public Task TaskGive;
    public Task TaskTake;
    public Task TaskCook;
    public Task TaskAction;
    public InventoryObject Item;
    public List<Pizza> Pizzas;
    public CookedInventoryObject CookedInventoryObject;
    public float WaitingTimeBeforeAction;
    public ParticleSystem Particles;
    public abstract Type typeOfNeededItem { get; }

    protected void OpenButton(Vector3 buttonPosition, Sprite icon)
    {
        var newButtonCanvas = Instantiate(actionButtonCanvas, transform);
        newButtonCanvas.transform.localPosition = buttonPosition;
        newButtonCanvas.transform.localScale = Vector2.one * iconScale;
        newButtonCanvas.ActionButton.image.sprite = icon;
    }

    public void Give(InventoryObject inventoryObject, int amount = 1)
    {
        Inventory.Instance.CreateOrAddObject(inventoryObject, amount);
    }

    public void Take(InventoryObject inventoryObject, int amount = 1)
    {
        if (!Inventory.Instance.TryTakeOrRemoveObject(inventoryObject, amount))
        {
            CancelAction();
        }
    }

    public abstract void Interact();
    public abstract void StartAction();
    public abstract void CancelAction();
    public abstract void ItemDownCast();

    public void SetItem(InventoryObject inventoryObject)
    {
        Item = inventoryObject;
    }

    public virtual void DoAfterTimer()
    {
        return;
    }

    protected void OpenInventoryField()
    {
        WindowsController.Instance.OpenWindow(window, this);
    }
}