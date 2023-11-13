using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryField : MonoBehaviour
{
    [SerializeField] private GameObject inventoryField;
    [SerializeField] private ItemPanel itemPanel;
    [SerializeField] private GameObject emptyInventoryText;
    [SerializeField] private GameObject cancelPanel;
    private ActionObject actionObjectCallBack;
    public static InventoryField Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenInventoryField(ActionObject actionObject)
    {
        CloseInventoryField();
        actionObjectCallBack = actionObject;
        inventoryField.SetActive(true);
        cancelPanel.SetActive(true);
        LoadItems();
    }

    private void LoadItems()
    {
        var allItems = Inventory.Instance.GetAllItems()
            .Where(item => item.Key.GetType() == actionObjectCallBack.typeOfNeededItem);
        if (allItems.Any())
            foreach (var item in allItems)
            {
                var newItemPanel = Instantiate(itemPanel, inventoryField.transform);
                newItemPanel.FillData(item.Key, item.Value);
                newItemPanel.ActionObjectCallBack = actionObjectCallBack;
            }
        else
        {
            emptyInventoryText.SetActive(true);
            cancelPanel.SetActive(true);
        }
    }

    public void UpdateItems()
    {
        if (inventoryField.activeSelf)
        {
            if (actionObjectCallBack != null)
                OpenInventoryField(actionObjectCallBack);
            else
                ShowFullInventory();
        }
    }

    public void ShowFullInventory()
    {
        CloseInventoryField();
        inventoryField.SetActive(true);
        cancelPanel.SetActive(true);
        var allItems = Inventory.Instance.GetSortedItemsByAmount();
        foreach (var item in allItems)
        {
            var newItemPanel = Instantiate(itemPanel, inventoryField.transform);
            newItemPanel.FillData(item.Key, item.Value);
            newItemPanel.GetComponent<Button>().interactable = false;
        }
    }

    public void OpenCloseFullInventory()
    {
        if (!inventoryField.activeSelf)
            ShowFullInventory();
        else
            CloseInventoryField();
    }

    public void CancelInventoryField()
    {
        if (actionObjectCallBack != null)
            actionObjectCallBack.CancelAction();
        CloseInventoryField();
    }

    public void CloseInventoryField()
    {
        if (inventoryField.activeSelf)
        {
            foreach (Transform item in inventoryField.transform)
                Destroy(item.gameObject);

            actionObjectCallBack = null;
            inventoryField.SetActive(false);
            emptyInventoryText.SetActive(false);
            cancelPanel.SetActive(false);
        }
    }
}