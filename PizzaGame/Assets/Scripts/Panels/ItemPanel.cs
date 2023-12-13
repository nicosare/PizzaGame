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
    [SerializeField] private TextMeshProUGUI cost;
    public ActionObject ActionObjectCallBack;
    public InventoryObject InventoryObject;
    public int AmountOfItems;
    private Window inventoryWindow;

    private void Start()
    {
        inventoryWindow = transform.GetComponentInParent<Window>();
    }

    public void FillData(InventoryObject item, int amount)
    {
        InventoryObject = item;
        AmountOfItems = amount;
        itemIcon.sprite = item.Icon;
        itemName.text = item.nameOfObject;
        itemAmount.text = amount.ToString();
        if (cost != null)
            cost.text = item.Cost.ToString();
    }

    public void TakeFromInventory()
    {
        TaskManager.Instance.CreateTask(ActionObjectCallBack.TaskTake, ActionObjectCallBack, InventoryObject);
        WindowsController.Instance.CloseWindow(inventoryWindow);
    }

    public void Buy()
    {
        if (MoneyManager.Instance.GetBalance() >= InventoryObject.Cost)
        {
            MoneyManager.Instance.TakeMoney(InventoryObject.Cost);
            Inventory.Instance.CreateOrAddObject(InventoryObject, 1);
            Shop.Instance.Buy(InventoryObject, 1);
            WindowsController.Instance.UpdateWindow(inventoryWindow);
        }
    }
}