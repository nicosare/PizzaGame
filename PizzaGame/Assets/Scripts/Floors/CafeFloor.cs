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
            (CustomersWaitLongerUpgrade, $"���������� ���� ������ � {waitInQueueTimeScale} ����"),
            (OpenNewInteriorUpgrade, $"����� ��������"),
            (TakeMoreOrdersUpgrade, $"���������� ��������� �������� �������: {OrderController.Instance.MaxAmountOfOrders + 1}"),
            (AddRatingUpgrade, $"������� + {ratingAmount}")
        };
        SecondUpgrade = new List<(Upgrade upgrade, string upgradeInfo)>
        {
            (CustomersWaitLongerUpgrade, $"���������� ���� ������ � {waitInQueueTimeScale*waitInQueueTimeScale} ����"),
            (OpenNewInteriorUpgrade, $"����� ��������"),
            (TakeMoreOrdersUpgrade, $"���������� ��������� �������� �������: {OrderController.Instance.MaxAmountOfOrders + 2}"),
            (AddRatingUpgrade, $"������� + {ratingAmount*ratingUpScale}")
        };
        ThirdUpgrade = new List<(Upgrade upgrade, string upgradeInfo)>
        {
            (CustomersWaitLongerUpgrade, $"���������� ���� ������ � {waitInQueueTimeScale*waitInQueueTimeScale*waitInQueueTimeScale} ����"),
            (OpenNewInteriorUpgrade, $"����� ��������"),
            (TakeMoreOrdersUpgrade, $"���������� ��������� �������� �������: {OrderController.Instance.MaxAmountOfOrders + 3}"),
            (AddRatingUpgrade, $"������� + {ratingAmount*ratingUpScale*ratingUpScale}")
        };
    }

    private void CustomersWaitLongerUpgrade()
    {
        CustomersManager.Instance.WaitInQueueScale *= waitInQueueTimeScale;
    }

    private void OpenNewInteriorUpgrade()
    {
        //TODO �������� ��������, ������� ����� �����������
        foreach (var obj in interior.Take(interior.Count * FloorLevel / 3))
            obj.SetActive(true);
    }

    private void TakeMoreOrdersUpgrade()
    {
        OrderController.Instance.MaxAmountOfOrders++;
    }
}
