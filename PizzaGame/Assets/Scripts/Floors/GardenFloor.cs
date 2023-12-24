using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Android;

public class GardenFloor : Floor
{
    [SerializeField] private List<GardenBed> gardenBeds;
    [SerializeField] private float gardenBedTimeScale;
    [SerializeField] private float upChanceToReturnSeeds;
    private int availableGardenBedsCount = 2;

    protected override void CreateUpgrades()
    {
        for (var i = 0; i < availableGardenBedsCount && i < gardenBeds.Count; i++)
            gardenBeds[i].gameObject.SetActive(true);

        FirstUpgrade = new List<(Upgrade upgrade, string upgradeInfo)>
        {
            (AddGardenBedUpgrade, $"Количество грядок: {availableGardenBedsCount+1}"),
            (SpeedUpGrowTimeUpgrade, $"Семена растут в {gardenBedTimeScale} раза быстрее"),
            (MoreIngredientsUpgrade, $"Больше собираемых ингредиентов"),
            (AddRatingUpgrade, $"Рейтинг + {ratingAmount}")
        };

        SecondUpgrade = new List<(Upgrade upgrade, string upgradeInfo)>
        {
            (AddGardenBedUpgrade, $"Количество грядок: {availableGardenBedsCount+2}"),
            (SpeedUpGrowTimeUpgrade, $"Семена растут в {gardenBedTimeScale*gardenBedTimeScale} раза быстрее"),
            (UpChanceToReturnSeeds, $"Шанс на возврат семян + {upChanceToReturnSeeds}%"),
            (AddRatingUpgrade, $"Рейтинг + {ratingAmount*ratingUpScale}")
        };

        ThirdUpgrade = new List<(Upgrade upgrade, string upgradeInfo)>
        {
            (AddGardenBedUpgrade, $"Количество грядок: {availableGardenBedsCount+3}"),
            (SpeedUpGrowTimeUpgrade, $"Семена растут в {gardenBedTimeScale*gardenBedTimeScale*gardenBedTimeScale} раза быстрее"),
            (MoreIngredientsUpgrade, $"Больше собираемых ингредиентов"),
            (AddRatingUpgrade, $"Рейтинг + {ratingAmount*ratingUpScale*ratingUpScale}")
        };
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

    private void UpChanceToReturnSeeds()
    {
        foreach (var gardenBed in gardenBeds)
            gardenBed.ChanceToReturnSeed += upChanceToReturnSeeds;
    }
}
