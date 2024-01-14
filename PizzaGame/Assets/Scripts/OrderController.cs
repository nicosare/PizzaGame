using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    public static OrderController Instance;
    public List<Order> Orders;
    public int MaxAmountOfOrders;
    public Order FirstActiveOrder;
    public Order SecondActiveOrder;

    private void Awake()
    {
        SecondActiveOrder = null;
        FirstActiveOrder = null;
        Orders = new List<Order>();
        Instance = this;
    }
}
