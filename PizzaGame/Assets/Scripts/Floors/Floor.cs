using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Floor : MonoBehaviour
{
    [SerializeField] private UpgradeFloorWindow window;
    [SerializeField] protected int ratingAmount;
    [SerializeField] protected int ratingUpScale;
    public int FloorLevel;
    public delegate void Upgrade();
    public List<(Upgrade upgrade, string upgradeInfo)> FirstUpgrade;
    public List<(Upgrade upgrade, string upgradeInfo)> SecondUpgrade;
    public List<(Upgrade upgrade, string upgradeInfo)> ThirdUpgrade;
    public List<List<(Upgrade upgrade, string upgradeInfo)>> Upgrades;

    protected abstract void CreateUpgrades();
    protected abstract void SetLevel();

    private void Awake()
    {
        window.SetButtonStatus(FloorLevel);
        SetLevel();
        CreateUpgrades();
    }

    private void Start()
    {
        Upgrades = new List<List<(Upgrade upgrade, string upgradeInfo)>>() { FirstUpgrade, SecondUpgrade, ThirdUpgrade };
        if (FloorLevel > 0)
            LoadUpgrades();
    }

    private void LoadUpgrades()
    {
        for (var i = 0; i < FloorLevel; i++)
        {
            foreach (var upgrade in Upgrades[i])
                upgrade.upgrade.Invoke();
        }
    }

    public void UpgradeFloor()
    {
        if (FloorLevel < Upgrades.Count)
        {
            foreach (var upgrade in Upgrades[FloorLevel])
                upgrade.upgrade.Invoke();
            FloorLevel++;
            for (var i = 0; i < 3; i++)
                window.SetButtonStatus(i);
        }
    }

    protected void AddRatingUpgrade()
    {
        RatingManager.Instance.AddRating(ratingAmount);
        ratingAmount *= ratingUpScale;
    }
}
