using System;
using UnityEngine;

public class SliderCookObject : ActionObject
{
    public override Type typeOfNeededItem => throw new NotImplementedException();
    [SerializeField] private Sprite createIcon;


    private void Awake()
    {
        OpenButton(spawnPosition, createIcon);
    }

    public override void CancelAction()
    {
        OpenButton(spawnPosition, createIcon);
    }

    public override void Interact()
    {
        OpenSlider();
    }

    public override void ItemDownCast()
    {
        throw new NotImplementedException();
    }

    public override void StartAction()
    {
        OpenButton(spawnPosition, createIcon);
    }

    public void OpenSlider()
    {
        WindowsController.Instance.OpenWindow(window, this);
    }
}
