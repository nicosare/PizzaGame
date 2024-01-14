using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<InventoryObject, int> inventory = new Dictionary<InventoryObject, int>();
    public static Inventory Instance;
    public List<Window> Windows;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void Initialization()
    {
        var sliderCreateWindow = FindObjectOfType<SliderCreateWindow>();
        if (!Windows.Contains(sliderCreateWindow) && sliderCreateWindow != null)
            Windows.Add(sliderCreateWindow);
        var fullInventoryWindow = FindObjectOfType<FullInventoryWindow>();
        if (!Windows.Contains(fullInventoryWindow) && fullInventoryWindow != null)
            Windows.Add(fullInventoryWindow);
    }

    public void CreateOrAddObject(InventoryObject inventoryObject, int amount)
    {
        if (inventory.ContainsKey(inventoryObject))
            inventory[inventoryObject] += amount;
        else
            inventory.Add(inventoryObject, amount);

        if (inventoryObject is not Pizza)
            ShowItemManager.Instance.ShowTakeItem(inventoryObject.Icon, inventoryObject.nameOfObject, amount);

        foreach (var window in Windows)
            WindowsController.Instance.UpdateWindow(window);
    }

    public bool TryTakeOrRemoveObject(InventoryObject inventoryObject, int amount)
    {
        if (CheckEnoughAmountObjects(inventoryObject, amount))
        {
            if (inventory[inventoryObject] == amount)
                inventory.Remove(inventoryObject);
            else
                inventory[inventoryObject] -= amount;

            if (inventoryObject is not Pizza)
                ShowItemManager.Instance.ShowGiveItem(inventoryObject.Icon, inventoryObject.nameOfObject, amount);

            foreach (var window in Windows)
                WindowsController.Instance.UpdateWindow(window);

            return true;
        }

        return false;
    }

    public bool CheckEnoughAmountObjects(InventoryObject inventoryObject, int amount)
    {
        return inventory.ContainsKey(inventoryObject) && inventory[inventoryObject] >= amount;
    }

    public int GetAmountOfObject(InventoryObject inventoryObject)
    {
        if (inventory.ContainsKey(inventoryObject))
            return inventory[inventoryObject];
        else return 0;
    }

    public Dictionary<InventoryObject, int> GetAllItems()
    {
        return inventory;
    }

    public Dictionary<InventoryObject, int> GetSortedItemsByAmount()
    {
        return inventory.OrderByDescending(item => item.Value)
            .Where(item => item.Key is not Pizza)
            .ToDictionary(item => item.Key, item => item.Value);
    }
}