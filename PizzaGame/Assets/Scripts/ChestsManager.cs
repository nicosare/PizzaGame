using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChestsManager : MonoBehaviour
{
    [SerializeField] private List<InventoryObject> ItemsToGive;
    public static ChestsManager Instance;
    public int MinAmountItems;
    public int MaxAmountItems;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public InventoryObject ChooseItem()
    {
        return ItemsToGive.Shuffle().First();
    }
}
