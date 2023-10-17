using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Exception = System.Exception;

public class ActionObject : MonoBehaviour
{
    [SerializeField] private ActionButtonCanvas actionButtonCanvas;
    [SerializeField] protected Vector3 spawnPosition;

    protected void OpenButton(Vector3 buttonPosition, Sprite icon)
    {
        var newButtonCanvas = Instantiate(actionButtonCanvas, transform);
        newButtonCanvas.transform.localPosition = buttonPosition;
        newButtonCanvas.ActionButton.image.sprite = icon;
    }

    protected void Give(InventoryObject inventoryObject, int amount = 1)
    {
        Inventory.Instance.CreateOrAddObject(inventoryObject, amount);
        Inventory.Instance.ShowInvenoryItems();
    }

    protected void Take(InventoryObject inventoryObject, int amount = 1)
    {
        if (Inventory.Instance.TryTakeOrRemoveObject(inventoryObject, amount))
            Inventory.Instance.ShowInvenoryItems();
        else
        {
            Debug.Log($"Недостаточно {inventoryObject}!");
        }
    }
}