using System.Collections.Generic;
using UnityEngine;

public abstract class Floor : MonoBehaviour
{
    [SerializeField] private UpgradeFloorWindow window;
    [SerializeField] protected int ratingAmount;
    [SerializeField] protected int ratingUpScale;
    public int index;
    public int FloorLevel;
    public delegate void Upgrade();
    public List<(Upgrade upgrade, string upgradeInfo)> FirstUpgrade;
    public List<(Upgrade upgrade, string upgradeInfo)> SecondUpgrade;
    public List<(Upgrade upgrade, string upgradeInfo)> ThirdUpgrade;
    public List<List<(Upgrade upgrade, string upgradeInfo)>> Upgrades;
    private Dictionary<int, bool> ratingLevels = new Dictionary<int, bool>();
    protected abstract void CreateUpgrades();

    public void SetLevel(int level)
    {
        FloorLevel = level;
    }

    private void Awake()
    {
        SetLevel(FloorsManager.FloorLevels[index]);
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
            window.SetButtonStatus(i);
        }
    }

    public void UpgradeFloor()
    {
        if (FloorLevel < Upgrades.Count)
        {
            if (!ratingLevels.ContainsKey(FloorLevel))
                ratingLevels.Add(FloorLevel, true);

            foreach (var upgrade in Upgrades[FloorLevel])
                upgrade.upgrade.Invoke();

            FloorLevel++;
            FloorsManager.FloorLevels[index] = FloorLevel;

            for (var i = 0; i < 3; i++)
                window.SetButtonStatus(i);
        }
    }

    protected void AddRatingUpgrade()
    {
        if (ratingLevels.ContainsKey(FloorLevel) && ratingLevels[FloorLevel])
            RatingManager.Instance.AddRating(ratingAmount);
        ratingAmount *= ratingUpScale;
    }
}
