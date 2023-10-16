using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionObject : MonoBehaviour
{
    [SerializeField] private ActionButton actionButton;

    protected void OpenButton(Vector3 buttonPosition, Sprite icon)
    {
        var newButton = Instantiate(actionButton, transform);
        newButton.transform.localPosition = buttonPosition;
        newButton.transform.GetChild(0).GetComponent<Button>().image.sprite = icon;
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