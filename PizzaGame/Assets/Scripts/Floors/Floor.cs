using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Floor : MonoBehaviour
{
    [SerializeField] private int ratingAmount;
    protected int floorLevel;
    protected delegate void Upgrade();
    protected List<Upgrade> firstUpgrade;
    protected List<Upgrade> secondUpgrade;
    protected List<Upgrade> thirdUpgrade;
    private List<List<Upgrade>> upgrades;

    protected abstract void CreateUpgrades();
    protected abstract void SetLevel();

    private void Awake()
    {
        SetLevel();
        CreateUpgrades();
    }

    private void Start()
    {
        upgrades = new List<List<Upgrade>>() { firstUpgrade, secondUpgrade, thirdUpgrade };
        if (floorLevel > 0)
            LoadUpgrades();
    }

    private void LoadUpgrades()
    {
        for (var i = 0; i < floorLevel; i++)
        {
            foreach (var upgrade in upgrades[i])
                upgrade.Invoke();
        }
    }

    public void UpgradeFloor()
    {
        if (floorLevel < upgrades.Count)
        {
            foreach (var upgrade in upgrades[floorLevel])
                upgrade.Invoke();
            floorLevel++;
        }
    }

    protected void AddRatingUpgrade()
    {
        RatingManager.Instance.AddRating(ratingAmount);
    }
}
