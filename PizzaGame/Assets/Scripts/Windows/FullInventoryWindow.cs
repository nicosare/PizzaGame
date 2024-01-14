using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FullInventoryWindow : Window
{
    [SerializeField] private ItemPanel itemPanel;
    [SerializeField] private List<Button> buttons;
    [SerializeField] private Sprite selectedButtonImage;
    [SerializeField] private Sprite buttonImage;
    [SerializeField] private Shop shop;

    public override void StartAction(ActionObject actionObject)
    {
        if (actionObject != null)
            ActionObjectCallBack = actionObject;
        if (actionObject is GardenBed)
        {
            ShowOnly(actionObject.typeOfNeededItem);
            buttons[2].image.sprite = selectedButtonImage;
        }
        else
        {
            ShowFullInventory();
            buttons[0].image.sprite = selectedButtonImage;
        }
    }

    public void ShowFullInventory()
    {
        SetButtonSprite();
        foreach (Transform item in windowField.transform)
            Destroy(item.gameObject);

        var allItems = shop == null ? Inventory.Instance.GetSortedItemsByAmount() : shop.ShopItems;
        foreach (var item in allItems)
        {
            var newItemPanel = Instantiate(itemPanel, windowField.transform);
            newItemPanel.FillData(item.Key, item.Value);

            if (ActionObjectCallBack != null)
                newItemPanel.ActionObjectCallBack = ActionObjectCallBack;

            if (ActionObjectCallBack != null)
            {
                if (item.Key.GetType() == ActionObjectCallBack.typeOfNeededItem)
                    newItemPanel.GetComponent<Button>().interactable = true;
                else
                    newItemPanel.GetComponent<Button>().interactable = false;
            }
            else
                newItemPanel.GetComponent<Button>().interactable = false;
            if (shop != null)
            {
                if (MoneyManager.Instance.GetBalance() >= item.Key.Cost)
                    newItemPanel.GetComponent<Button>().interactable = true;
                else
                    newItemPanel.GetComponent<Button>().interactable = false;
            }
        }
    }

    private void ShowOnly(Type type)
    {
        SetButtonSprite();
        foreach (Transform item in windowField.transform)
            Destroy(item.gameObject);

        var allItems = shop == null ? Inventory.Instance.GetSortedItemsByAmount()
            .Where(item => item.Key.GetType() == type) :
            shop.ShopItems.Where(item => item.Key.GetType() == type);
        if (allItems.Any())
            foreach (var item in allItems)
            {
                var newItemPanel = Instantiate(itemPanel, windowField.transform);
                newItemPanel.FillData(item.Key, item.Value);
                newItemPanel.GetComponent<Button>().interactable = true;
                newItemPanel.ActionObjectCallBack = ActionObjectCallBack;
            }
        else
        {
            Debug.Log("Empty!");
        }
    }

    public void ShowOnly(InventoryObject inventoryObject)
    {
        SetButtonSprite();
        foreach (Transform item in windowField.transform)
            Destroy(item.gameObject);

        var allItems = shop == null ? Inventory.Instance.GetSortedItemsByAmount()
            .Where(item => item.Key.GetType() == inventoryObject.GetType())
            : shop.ShopItems.Where(item => item.Key.GetType() == inventoryObject.GetType());
        if (allItems.Any())
            foreach (var item in allItems)
            {
                var newItemPanel = Instantiate(itemPanel, windowField.transform);
                newItemPanel.FillData(item.Key, item.Value);
                if (ActionObjectCallBack != null)
                    newItemPanel.ActionObjectCallBack = ActionObjectCallBack;
                if (ActionObjectCallBack != null)
                {
                    if (item.Key.GetType() == ActionObjectCallBack.typeOfNeededItem)
                        newItemPanel.GetComponent<Button>().interactable = true;
                    else
                        newItemPanel.GetComponent<Button>().interactable = false;
                }
                else
                    newItemPanel.GetComponent<Button>().interactable = false;
                if (shop != null)
                {
                    if (MoneyManager.Instance.GetBalance() >= item.Key.Cost)
                        newItemPanel.GetComponent<Button>().interactable = true;
                    else
                        newItemPanel.GetComponent<Button>().interactable = false;
                }
            }
        else
        {
            Debug.Log("Empty!");
        }
    }

    public void SetButtonSprite()
    {
        foreach (var button in buttons)
            button.image.sprite = buttonImage;
    }

    public override void UpdateWindow()
    {
        if (windowField.activeSelf)
            StartAction(ActionObjectCallBack);
    }

    public override void CloseWindow()
    {
        if (windowField.activeSelf)
        {
            foreach (Transform item in windowField.transform)
                Destroy(item.gameObject);
            gameObject.SetActive(false);
            ActionObjectCallBack = null;
        }
    }

    public override void CancelWindow()
    {
        foreach (Transform item in windowField.transform)
            Destroy(item.gameObject);
        gameObject.SetActive(false);
        if (ActionObjectCallBack != null)
            ActionObjectCallBack.CancelAction();
        ActionObjectCallBack = null;
    }
}
