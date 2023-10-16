using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ActionObject : MonoBehaviour
{
    [FormerlySerializedAs("actionButton")] [SerializeField] private ActionButtonCanvas actionButtonCanvas;
    [SerializeField] protected Vector3 spawnPosition;


    protected void OpenButton(Vector3 buttonPosition, Sprite icon)
    {
        var newButtonCanvas = Instantiate(actionButtonCanvas, transform);
        newButtonCanvas.transform.localPosition = buttonPosition;
        newButtonCanvas.ActionButton.image.sprite = icon;
    }

    protected void Give(InventoryObject inventoryObject, int amount = 1)
    {
        Debug.Log($"{amount} of {inventoryObject.nameOfObject} given to inventory");
        //TODO Реализовать инвентарь и добавить туда amount объектов
    }

    protected void Take(InventoryObject inventoryObject, int amount = 1)
    {
        Debug.Log($"{amount} of {inventoryObject.nameOfObject} taken from inventory");
        //TODO Реализовать инвентарь и забрать оттуда amount объектов, если есть
    }
}