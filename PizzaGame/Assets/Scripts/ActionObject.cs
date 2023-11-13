using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class ActionObject : MonoBehaviour, IInteractable
{
    [SerializeField] private ActionButtonCanvas actionButtonCanvas;
    [SerializeField] protected Vector3 spawnPosition;
    public Task TaskGive;
    public Task TaskTake;
    public InventoryObject Item;
    public abstract Type typeOfNeededItem { get; }

    protected void OpenButton(Vector3 buttonPosition, Sprite icon)
    {
        var newButtonCanvas = Instantiate(actionButtonCanvas, transform);
        newButtonCanvas.transform.localPosition = buttonPosition;
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
            Debug.Log($"Недостаточно {inventoryObject}!");
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

    protected void OpenInventoryField()
    {
        InventoryField.Instance.OpenInventoryField(this);
    }
}