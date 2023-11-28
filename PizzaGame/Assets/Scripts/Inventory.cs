using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<InventoryObject, int> inventory = new Dictionary<InventoryObject, int>();
    public static Inventory Instance;
    [SerializeField] private List<Window> windows;
    [SerializeField] private InventoryObject[] testGiveObject;

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

    private void Start()
    {
        foreach (var item in testGiveObject)
        {
            CreateOrAddObject(item, 5);
        }
    }

    public void CreateOrAddObject(InventoryObject inventoryObject, int amount)
    {
        if (inventory.ContainsKey(inventoryObject))
            inventory[inventoryObject] += amount;
        else
            inventory.Add(inventoryObject, amount);

        foreach (var window in windows)
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
            
            foreach (var window in windows)
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