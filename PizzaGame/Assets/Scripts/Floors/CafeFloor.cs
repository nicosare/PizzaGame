using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CafeFloor : Floor
{
    [SerializeField] private float waitInQueueTimeScale;
    [SerializeField] private List<GameObject> interior;

    protected override void CreateUpgrades()
    {
        FirstUpgrade = new List<(Upgrade upgrade, string upgradeInfo)>
        {
            (CustomersWaitLongerUpgrade, $"Посетители ждут дольше в {waitInQueueTimeScale} раза"),
            (OpenNewInteriorUpgrade, $"Новый интерьер"),
            (TakeMoreOrdersUpgrade, $"Количество возможных активных заказов: {OrderController.Instance.MaxAmountOfOrders + 1}"),
            (AddRatingUpgrade, $"Рейтинг + {ratingAmount}")
        };
        SecondUpgrade = new List<(Upgrade upgrade, string upgradeInfo)>
        {
            (CustomersWaitLongerUpgrade, $"Посетители ждут дольше в {waitInQueueTimeScale*waitInQueueTimeScale} раза"),
            (OpenNewInteriorUpgrade, $"Новый интерьер"),
            (TakeMoreOrdersUpgrade, $"Количество возможных активных заказов: {OrderController.Instance.MaxAmountOfOrders + 2}"),
            (AddRatingUpgrade, $"Рейтинг + {ratingAmount*ratingUpScale}")
        };
        ThirdUpgrade = new List<(Upgrade upgrade, string upgradeInfo)>
        {
            (CustomersWaitLongerUpgrade, $"Посетители ждут дольше в {waitInQueueTimeScale*waitInQueueTimeScale*waitInQueueTimeScale} раза"),
            (OpenNewInteriorUpgrade, $"Новый интерьер"),
            (TakeMoreOrdersUpgrade, $"Количество возможных активных заказов: {OrderController.Instance.MaxAmountOfOrders + 3}"),
            (AddRatingUpgrade, $"Рейтинг + {ratingAmount*ratingUpScale*ratingUpScale}")
        };
    }

    private void CustomersWaitLongerUpgrade()
    {
        CustomersManager.Instance.WaitInQueueScale *= waitInQueueTimeScale;
    }

    private void OpenNewInteriorUpgrade()
    {
        //TODO Добавить интерьер, который будет открываться
        foreach (var obj in interior.Take(interior.Count * FloorLevel / 3))
            obj.SetActive(true);
    }

    private void TakeMoreOrdersUpgrade()
    {
        OrderController.Instance.MaxAmountOfOrders++;
    }
}
