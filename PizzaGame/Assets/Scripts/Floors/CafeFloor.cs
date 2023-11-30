using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CafeFloor : Floor
{
    [SerializeField] private float waitInQueueTimeScale;

    protected override void CreateUpgrades()
    {
        firstUpgrade = new List<Upgrade>
        {
            CustomersWaitLongerUpgrade,
            AddRatingUpgrade
        };
        secondUpgrade = new List<Upgrade>
        {
            CustomersWaitLongerUpgrade,
            AddRatingUpgrade
        };
        thirdUpgrade = new List<Upgrade>
        {
            CustomersWaitLongerUpgrade,
            AddRatingUpgrade
        };
    }

    protected override void SetLevel()
    {
        floorLevel = 1;
    }
    private void CustomersWaitLongerUpgrade()
    {
        CustomersManager.Instance.WaitInQueueScale *= waitInQueueTimeScale;
    }
}
