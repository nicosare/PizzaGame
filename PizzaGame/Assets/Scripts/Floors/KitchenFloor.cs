using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KitchenFloor : Floor
{
    [SerializeField] private List<Furnace> furnaces;
    [SerializeField] private float cookTimeScale;
    [SerializeField] private Sink sink;
    [SerializeField] private float fillWaterTimeScale;
    [SerializeField] private int upWaterAmount;

    protected override void CreateUpgrades()
    {
        FirstUpgrade = new List<(Upgrade upgrade, string upgradeInfo)>
        {
            (SpeedUpCookTimeUpgrade, $"Пицца готовится в {cookTimeScale} раза быстрее"),
            (TakeMoreWaterUpgrade, $"Лимит количества воды: {sink.MaxCountWater+upWaterAmount}л"),
            (AddRatingUpgrade, $"Рейтинг + {ratingAmount}")
        };

        SecondUpgrade = new List<(Upgrade upgrade, string upgradeInfo)>
        {
            (SpeedUpCookTimeUpgrade, $"Пицца готовится в {cookTimeScale*cookTimeScale} раза быстрее"),
            (SpeedUpTakeWater, $"Скорость набора воды в {fillWaterTimeScale} раза быстрее"),
            (AddRatingUpgrade, $"Рейтинг + {ratingAmount*ratingUpScale}")
        };

        ThirdUpgrade = new List<(Upgrade upgrade, string upgradeInfo)>
        {
            (SpeedUpCookTimeUpgrade, $"Пицца готовится в {cookTimeScale*cookTimeScale*cookTimeScale} раза быстрее"),
            (TakeMoreWaterUpgrade, $"Лимит количества воды: {sink.MaxCountWater + upWaterAmount*2}л"),
            (OpenSecondFurnaceUpgrade, $"Дополнительная печь"),
            (AddRatingUpgrade, $"Рейтинг + {ratingAmount*ratingUpScale*ratingUpScale}")
        };
    }

    private void SpeedUpCookTimeUpgrade()
    {
        foreach (var furnace in furnaces)
            furnace.TimeScaleToCook *= cookTimeScale;
    }

    private void OpenSecondFurnaceUpgrade()
    {
        StartCoroutine(ChangeFurnaces());
    }

    private void TakeMoreWaterUpgrade()
    {
        sink.MaxCountWater += upWaterAmount;
    }

    private void SpeedUpTakeWater()
    {
        sink.FillSpeed *= fillWaterTimeScale;
    }

    private IEnumerator ChangeFurnaces()
    {
        yield return new WaitUntil(() => furnaces.All(furnace => furnace.CookedInventoryObject == null) && !TaskManager.Instance.BlockCreateTask);
        furnaces[0].gameObject.SetActive(false);
        furnaces[1].transform.parent.gameObject.SetActive(true);
    }
}
