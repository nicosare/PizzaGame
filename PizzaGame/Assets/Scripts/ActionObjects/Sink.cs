using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sink : ActionObject
{
    public override Type typeOfNeededItem => throw new NotImplementedException();
    [SerializeField] private Sprite takeWaterIcon;
    [SerializeField] private InventoryObject water;
    [SerializeField] private TextMeshProUGUI waterCountText;
    public int MaxCountWater;
    public float FillSpeed;
    private Guid taskUID;

    private void Awake()
    {
        OpenButton(spawnPosition, takeWaterIcon);
    }

    public override void CancelAction()
    {
        waterCountText.gameObject.SetActive(false);
        OpenButton(spawnPosition, takeWaterIcon);
    }

    public override void Interact()
    {
        StartFillWater();
    }

    private void StartFillWater()
    {
        if (!TaskManager.Instance.BlockCreateTask)
        {
            if (Inventory.Instance.GetAmountOfObject(water) < MaxCountWater)
            {
                waterCountText.gameObject.SetActive(true);
                ChangeWaterCounter();
                taskUID = TaskManager.Instance.CreateTask(TaskGive, this, water, 1);
            }
            else
            {
                Debug.Log("Water is full!");
                StartCoroutine(ShowCountInSeconds(1));
                OpenButton(spawnPosition, takeWaterIcon);
            }
        }
        else CancelAction();
    }

    private IEnumerator Filling()
    {
        ChangeWaterCounter();
        while (Inventory.Instance.GetAmountOfObject(water) < MaxCountWater)
        {
            ChangeWaterCounter();
            yield return new WaitForSeconds(1 / FillSpeed);
            if (!TaskManager.Instance.CheckActualTask(taskUID))
                break;
            Give(water, 1);
        }
        StartCoroutine(ShowCountInSeconds(1));
        OpenButton(spawnPosition, takeWaterIcon);
    }

    public override void ItemDownCast()
    {
        throw new NotImplementedException();
    }

    public override void StartAction()
    {
        StartCoroutine(Filling());
    }

    private void ChangeWaterCounter()
    {
        if (Inventory.Instance.GetAmountOfObject(water) < MaxCountWater)
            waterCountText.text = $"{Inventory.Instance.GetAmountOfObject(water)}/{MaxCountWater}ë";
        else
            StartCoroutine(ShowCountInSeconds(1));
    }

    private IEnumerator ShowCountInSeconds(float seconds)
    {
        waterCountText.gameObject.SetActive(true);
        waterCountText.text = $"{Inventory.Instance.GetAmountOfObject(water)}/{MaxCountWater}ë";
        yield return new WaitForSeconds(seconds);
        waterCountText.gameObject.SetActive(false);
    }
}
