using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Inventory
{
    private static List<InventoryObject> invenory;

    public static void AddObject(InventoryObject inventoryObject, int amount)
    {
        for (var i = 0; i < amount; i++)
            invenory.Add(inventoryObject);
    }

    public static void TakeObject(InventoryObject inventoryObject, int amount)
    {
        if(invenory.Contains(inventoryObject))
            if(i)
    }
}