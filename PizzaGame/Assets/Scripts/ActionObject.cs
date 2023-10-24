using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class ActionObject : MonoBehaviour
{
    [SerializeField] private ActionButtonCanvas actionButtonCanvas;
    [SerializeField] protected Vector3 spawnPosition;
    public InventoryObject Item;
    public abstract Type typeOfNeededItem { get; }

    protected void OpenButton(Vector3 buttonPosition, Sprite icon)
    {
        var newButtonCanvas = Instantiate(actionButtonCanvas, transform);
        newButtonCanvas.transform.localPosition = buttonPosition;
        newButtonCanvas.ActionButton.image.sprite = icon;
    }

    protected void Give(InventoryObject inventoryObject, int amount = 1)
    {
        Inventory.Instance.CreateOrAddObject(inventoryObject, amount);
    }

    public void Take(InventoryObject inventoryObject, int amount = 1)
    {
        if (!Inventory.Instance.TryTakeOrRemoveObject(inventoryObject, amount))
            Debug.Log($"Недостаточно {inventoryObject}!");
    }

    public abstract void StartAction();
    public abstract void CancelAction();
    public abstract void ItemDownCast();

    public void SetItem(InventoryObject inventoryObject)
    {
        Item = inventoryObject;
    }

    protected void OpenInventoryField()
    {
        InventoryField.Instance.OpenInventoryField(this);
    }
}