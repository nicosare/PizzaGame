using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Android;

public class GardenFloor : Floor
{
    [SerializeField] private List<GardenBed> gardenBeds;
    [SerializeField] private float gardenBedTimeScale;
    private int availableGardenBedsCount = 2;

    protected override void CreateUpgrades()
    {
        for (var i = 0; i < availableGardenBedsCount && i < gardenBeds.Count; i++)
            gardenBeds[i].gameObject.SetActive(true);

        firstUpgrade = new List<Upgrade>
        {
            AddGardenBedUpgrade,
            SpeedUpGrowTimeUpgrade,
            MoreIngredientsUpgrade,
            AddRatingUpgrade
        };

        secondUpgrade = new List<Upgrade>
        {
            AddGardenBedUpgrade,
            SpeedUpGrowTimeUpgrade,
            MoreIngredientsUpgrade,
            AddRatingUpgrade
        };

        thirdUpgrade = new List<Upgrade>
        {
            AddGardenBedUpgrade,
            SpeedUpGrowTimeUpgrade,
            MoreIngredientsUpgrade,
            AddRatingUpgrade
        };
    }

    protected override void SetLevel()
    {
        //TODO Подключиться к базе данных
        floorLevel = 0;
    }

    private void AddGardenBedUpgrade()
    {
        availableGardenBedsCount++;
        for (var i = 0; i < availableGardenBedsCount && i < gardenBeds.Count; i++)
            gardenBeds[i].gameObject.SetActive(true);
    }

    private void SpeedUpGrowTimeUpgrade()
    {
        foreach (var gardenBed in gardenBeds)
            gardenBed.TimeScaleToGrow *= gardenBedTimeScale;
    }

    private void MoreIngredientsUpgrade()
    {
        foreach (var gardenBed in gardenBeds)
            gardenBed.bonusIngredientCount++;
    }
}
