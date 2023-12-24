using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeFloorWindow : Window
{
    [SerializeField] private Floor floor;
    [SerializeField] private List<UpgradePanel> upgradePanels;

    private void LoadUpgrades(Floor floor)
    {
        for (var i = 0; i < 3; i++)
        {
            SetButtonStatus(i);
            upgradePanels[i].LoadData(i, floor.Upgrades[i]);
        }
    }

    public void SetButtonStatus(int buttonIndex)
    {
        var upgradePanel = upgradePanels[buttonIndex];
        if (buttonIndex < floor.FloorLevel)
        {
            upgradePanel.CostPanel.SetActive(false);
            upgradePanel.UpgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Улучшено";
            upgradePanel.UpgradeButton.enabled = false;
            upgradePanel.UpgradeButton.GetComponent<Image>().color = new Color(0, 1, 0, .4f);
        }
        else if (buttonIndex == floor.FloorLevel)
        {
            upgradePanel.UpgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Повысить";
            if (MoneyManager.Instance.GetBalance() >= upgradePanel.Cost)
                upgradePanel.UpgradeButton.enabled = true;
            else
            {
                upgradePanel.UpgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Недостаточно денег";
                upgradePanel.UpgradeButton.enabled = false;
                upgradePanel.UpgradeButton.GetComponent<Image>().color = new Color(1, 0, 0, .4f);
            }
            upgradePanel.UpgradeButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            upgradePanel.UpgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Недоступно";
            upgradePanel.UpgradeButton.enabled = false;
            upgradePanel.UpgradeButton.GetComponent<Image>().color = new Color(1, 0, 0, .4f);
        }
    }

    public override void StartAction(ActionObject actionObject)
    {
        LoadUpgrades(floor);
    }

    public override void UpdateWindow()
    {
        throw new System.NotImplementedException();
    }
}
