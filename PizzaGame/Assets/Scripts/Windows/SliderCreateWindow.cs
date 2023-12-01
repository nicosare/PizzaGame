using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderCreateWindow : Window
{
    [SerializeField] TakePanel takePanel;
    [SerializeField] Slider slider;
    [SerializeField] Image GiveIcon;
    [SerializeField] TextMeshProUGUI GiveText;
    [SerializeField] Button cookButton;
    private List<TakePanel> takePanels;
    private List<Ingredient> ingredients;
    private int previousValue;

    private void Awake()
    {
        previousValue = 0;
        takePanels = new List<TakePanel>();
        ingredients = new List<Ingredient>();
    }

    private void SetSlider()
    {
        slider.value = previousValue;
        slider.maxValue = GetMaxSlider();
    }

    private void LoadItems()
    {
        foreach (var ingredient in ActionObjectCallBack.CookedInventoryObject.ingredients)
        {
            var newPanel = Instantiate(takePanel, windowField.transform);
            newPanel.Icon.sprite = ingredient.Icon;
            newPanel.CountText.text = $"{ingredient.name} {slider.value}/{Inventory.Instance.GetAmountOfObject(ingredient)}";
            takePanels.Add(newPanel);
            ingredients.Add(ingredient);
        }
        GiveIcon.sprite = ActionObjectCallBack.CookedInventoryObject.Icon;
        UpdateValue();
    }

    public void UpdateValue()
    {
        for (var i = 0; i < takePanels.Count; i++)
        {
            takePanels[i].CountText.text = $"{slider.value}/{Inventory.Instance.GetAmountOfObject(ingredients[i])}";
            if (Inventory.Instance.GetAmountOfObject(ingredients[i]) == 0)
                cookButton.enabled = false;
            else
                cookButton.enabled = true;
        }
        GiveText.text = slider.value.ToString();
        previousValue = (int)slider.value;
    }

    private int GetMaxSlider()
    {
        var ingredients = ActionObjectCallBack.CookedInventoryObject.ingredients;
        return ingredients.Min(ingredient => Inventory.Instance.GetAmountOfObject(ingredient));
    }

    public void Cook()
    {
        TaskManager.Instance.CreateTask(ActionObjectCallBack.TaskCook, ActionObjectCallBack, ActionObjectCallBack.CookedInventoryObject, (int)slider.value);
    }

    public override void StartAction(ActionObject actionObject)
    {
        ActionObjectCallBack = actionObject;

        if (takePanels.Count > 0 && ActionObjectCallBack != null)
        {
            for (var i = 0; i < ActionObjectCallBack.CookedInventoryObject.ingredients.Count; i++)
                Destroy(windowField.transform.GetChild(windowField.transform.childCount - 1 - i).gameObject);
        }
        takePanels.Clear();
        ingredients.Clear();
        LoadItems();
        SetSlider();
    }

    public override void UpdateWindow()
    {
        if (windowField.activeSelf)
        {
            if (ActionObjectCallBack != null)
                StartAction(ActionObjectCallBack);
        }
    }

    public override void CloseWindow()
    {
        if (windowField.activeSelf && ActionObjectCallBack != null)
        {
            for (var i = 0; i < ActionObjectCallBack.CookedInventoryObject.ingredients.Count; i++)
                Destroy(windowField.transform.GetChild(windowField.transform.childCount - 1 - i).gameObject);
        }
        takePanels.Clear();
        ingredients.Clear();
        gameObject.SetActive(false);
        ActionObjectCallBack = null;
    }
}

