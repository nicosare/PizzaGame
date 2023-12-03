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
            upgradePanels[i].LoadData(i, floor.Upgrades[i]);
            SetButtonStatus(i);
        }
    }

    public void SetButtonStatus(int buttonIndex)
    {
        if (buttonIndex < floor.FloorLevel)
        {
            upgradePanels[buttonIndex].CostPanel.SetActive(false);
            upgradePanels[buttonIndex].UpgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Улучшено";
            upgradePanels[buttonIndex].UpgradeButton.enabled = false;
            upgradePanels[buttonIndex].UpgradeButton.GetComponent<Image>().color = new Color(0, 1, 0, .4f);
        }
        else if (buttonIndex == floor.FloorLevel)
        {
            upgradePanels[buttonIndex].UpgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Повысить";
            upgradePanels[buttonIndex].UpgradeButton.enabled = true;
            upgradePanels[buttonIndex].UpgradeButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            upgradePanels[buttonIndex].UpgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Недоступно";
            upgradePanels[buttonIndex].UpgradeButton.enabled = false;

            upgradePanels[buttonIndex].UpgradeButton.GetComponent<Image>().color = new Color(1, 0, 0, .4f);
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
