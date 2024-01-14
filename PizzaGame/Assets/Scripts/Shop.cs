using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<InventoryObject> inventoryObjects = new List<InventoryObject>();
    [SerializeField] private int minAmountOfAllItems;
    [SerializeField] private int minAmountOfOneItem;
    [SerializeField] private int maxAmountOfOneItem;

    public static Shop Instance;
    public Dictionary<InventoryObject, int> ShopItems = new Dictionary<InventoryObject, int>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;
        }

    }

    private void Start()
    {
        inventoryObjects.Shuffle();
        for (int i = 0; i <= Random.Range(minAmountOfAllItems, inventoryObjects.Count); i++)
        {
            ShopItems.Add(inventoryObjects[i], Random.Range(minAmountOfOneItem, maxAmountOfOneItem));
        }
    }

    public void Buy(InventoryObject inventoryObject, int amount)
    {
        if (CheckEnoughAmountObjects(inventoryObject, amount))
        {
            if (ShopItems[inventoryObject] == amount)
                ShopItems.Remove(inventoryObject);
            else
                ShopItems[inventoryObject] -= amount;
        }
    }

    public bool CheckEnoughAmountObjects(InventoryObject inventoryObject, int amount)
    {
        return ShopItems.ContainsKey(inventoryObject) && ShopItems[inventoryObject] >= amount;
    }
}
