using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FullInventoryWindow : Window
{
    [SerializeField] private ItemPanel itemPanel;
    [SerializeField] private List<Button> buttons;
    [SerializeField] private Sprite selectedButtonImage;
    [SerializeField] private Sprite buttonImage;
    public override void StartAction(ActionObject actionObject)
    {
        ShowFullInventory();
        buttons[0].image.sprite = selectedButtonImage;
    }

    public void ShowFullInventory()
    {
        SetButtonSprite();
        foreach (Transform item in windowField.transform)
            Destroy(item.gameObject);

        var allItems = Inventory.Instance.GetSortedItemsByAmount();
        foreach (var item in allItems)
        {
            var newItemPanel = Instantiate(itemPanel, windowField.transform);
            newItemPanel.FillData(item.Key, item.Value);
            newItemPanel.GetComponent<Button>().interactable = false;
        }
    }

    public void ShowOnly(InventoryObject inventoryObject)
    {
        SetButtonSprite();
        foreach (Transform item in windowField.transform)
            Destroy(item.gameObject);

        var allItems = Inventory.Instance.GetSortedItemsByAmount()
            .Where(item => item.Key.GetType() == inventoryObject.GetType());
        if (allItems.Any())
            foreach (var item in allItems)
            {
                var newItemPanel = Instantiate(itemPanel, windowField.transform);
                newItemPanel.FillData(item.Key, item.Value);
                newItemPanel.GetComponent<Button>().interactable = false;
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
        }
    }
}
