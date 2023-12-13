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
    [SerializeField] private List<InventoryObject> testGiveObject;

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
        testGiveObject.Shuffle();
        //for (var i = 0; i < UnityEngine.Random.Range(testGiveObject.Count, testGiveObject.Count); i++)
        //{

        //    CreateOrAddObject(testGiveObject[i], UnityEngine.Random.Range(5, 10));
        //}
    }

    public void CreateOrAddObject(InventoryObject inventoryObject, int amount)
    {
        if (inventory.ContainsKey(inventoryObject))
            inventory[inventoryObject] += amount;
        else
            inventory.Add(inventoryObject, amount);

        if (inventoryObject is not Pizza)
            ShowItemManager.Instance.ShowTakeItem(inventoryObject.Icon, inventoryObject.nameOfObject, amount);

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

            if (inventoryObject is not Pizza)
                ShowItemManager.Instance.ShowGiveItem(inventoryObject.Icon, inventoryObject.nameOfObject, amount);

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