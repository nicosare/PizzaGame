using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdersWindow : Window
{
    [SerializeField] private OrderPanel orderPanel;

    private void LoadItems()
    {
        foreach (var order in OrderController.Instance.Orders)
        {
            var newPanel = Instantiate(orderPanel, windowField.transform);
            newPanel.LoadData(order);
        }
    }

    public override void StartAction(ActionObject actionObject)
    {
        ActionObjectCallBack = actionObject;
        foreach (Transform item in windowField.transform)
            Destroy(item.gameObject);

        LoadItems();
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
        if (windowField.activeSelf)
        {
            foreach (Transform item in windowField.transform)
                Destroy(item.gameObject);
            gameObject.SetActive(false);
        }
        ActionObjectCallBack = null;
    }
}
