using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    public static OrderController Instance;
    public List<Order> Orders;
    public int MaxAmountOfOrders;
    public Order ActiveOrder;

    private void Awake()
    {
        ActiveOrder = null;
        Orders = new List<Order>();
        Instance = this;
    }
}
