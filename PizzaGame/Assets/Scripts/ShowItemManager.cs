using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemManager : MonoBehaviour
{
    public static ShowItemManager Instance;
    [SerializeField] private Transform itemField;
    [SerializeField] private ShowItemPanel showItemPanel;
    public TextMeshProUGUI ClockText;
    public TextMeshProUGUI CalendarText;
    public TextMeshProUGUI RatingText;
    public TextMeshProUGUI BalanceText;
    private List<ShowItemPanel> panels;

    private void Awake()
    {
        panels = new List<ShowItemPanel>();
        Instance = this;
    }


    private IEnumerator ShowItems()
    {
        foreach (var panel in panels.Where(panel => !panel.WasShown))
        {
            panel.gameObject.SetActive(true);
            yield return new WaitForSeconds(.3f);
        }
    }

    private IEnumerator WaitAllPanelsShow()
    {
        yield return new WaitUntil(() => panels.All(panel => panel.WasShown));
        foreach (Transform panel in itemField)
            Destroy(panel.gameObject);
        panels.Clear();
    }

    public void ShowTakeItem(Sprite icon, string name, int amount)
    {
        StopAllCoroutines();
        var newPanel = Instantiate(showItemPanel, itemField);
        newPanel.LoadTakeData(icon, name, amount);
        newPanel.gameObject.SetActive(false);
        panels.Add(newPanel);
        StartCoroutine(ShowItems());
        StartCoroutine(WaitAllPanelsShow());
    }

    public void ShowGiveItem(Sprite icon, string name, int amount)
    {
        StopAllCoroutines();
        var newPanel = Instantiate(showItemPanel, itemField);
        newPanel.LoadGiveData(icon, name, amount);
        newPanel.gameObject.SetActive(false);
        panels.Add(newPanel);
        StartCoroutine(ShowItems());
        StartCoroutine(WaitAllPanelsShow());
    }
}

