using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private int cost;
    public Button UpgradeButton;
    public GameObject CostPanel;

    public void LoadData(int level, List<(Floor.Upgrade, string)> upgrades)
    {
        levelText.text = $"Уровень {level + 1}";
        descriptionText.text = GetDescription(upgrades.Select(upgrade => upgrade.Item2).ToList());

    }

    private string GetDescription(List<string> descriptions)
    {
        var builder = new StringBuilder();
        foreach (var desc in descriptions)
        {
            builder.AppendLine("· " + desc.ToString());
        }
        return builder.ToString();
    }
}
