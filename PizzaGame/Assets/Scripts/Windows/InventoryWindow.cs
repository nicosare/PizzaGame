using System.Linq;
using UnityEngine;

public class InventoryWindow : Window
{
    [SerializeField] private ItemPanel itemPanel;

    private void LoadItems()
    {
        var allItems = Inventory.Instance.GetAllItems()
            .Where(item => item.Key.GetType() == ActionObjectCallBack.typeOfNeededItem);
        if (allItems.Any())
            foreach (var item in allItems)
            {
                var newItemPanel = Instantiate(itemPanel, windowField.transform);
                newItemPanel.FillData(item.Key, item.Value);
                newItemPanel.ActionObjectCallBack = ActionObjectCallBack;
            }
        else
        {
            Debug.Log("Empty!");
        }
    }

    public override void StartAction(ActionObject actionObject)
    {
        ActionObjectCallBack = actionObject;
        foreach (Transform item in windowField.transform)
            Destroy(item.gameObject);
        LoadItems();
    }

    public override void UpdateWindow()
    {
        if (windowField.activeSelf && ActionObjectCallBack != null)
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
        ActionObjectCallBack = null;
    }
}