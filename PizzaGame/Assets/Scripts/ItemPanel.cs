using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemAmount;

    public ActionObject ActionObjectCallBack;
    private InventoryObject inventoryObject;
    private int amountOfItems;
    private InventoryField inventoryField;

    private void Start()
    {
        inventoryField = transform.parent.parent.GetComponent<InventoryField>();
    }

    public void FillData(InventoryObject item, int amount)
    {
        inventoryObject = item;
        amountOfItems = amount;
        itemIcon.sprite = item.icon;
        itemName.text = item.nameOfObject;
        itemAmount.text = amount.ToString();
    }

    public void TakeFromInventory()
    {
        ActionObjectCallBack.SetItem(inventoryObject);
        ActionObjectCallBack.Take(inventoryObject);
        ActionObjectCallBack.StartAction();
        inventoryField.CloseInventoryField();
    }
}