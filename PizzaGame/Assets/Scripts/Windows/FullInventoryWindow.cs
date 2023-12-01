using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullInventoryWindow : Window
{
    [SerializeField] private ItemPanel itemPanel;

    public override void StartAction(ActionObject actionObject)
    {
        foreach (Transform item in windowField.transform)
            Destroy(item.gameObject);
        ShowFullInventory();
    }

    public void ShowFullInventory()
    {
        var allItems = Inventory.Instance.GetSortedItemsByAmount();
        foreach (var item in allItems)
        {
            var newItemPanel = Instantiate(itemPanel, windowField.transform);
            newItemPanel.FillData(item.Key, item.Value);
            newItemPanel.GetComponent<Button>().interactable = false;
        }
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
