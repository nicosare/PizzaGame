using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order
{
    public Pizza pizza;
    public Customer customer;

    public Order()
    {
        pizza = null;
        customer = null;
    }

    public Order(Order order)
    {
        pizza = order.pizza;
        customer = order.customer;
    }

    public Order(Pizza pizza, Customer customer)
    {
        this.pizza = pizza;
        this.customer = customer;
    }

    public override string ToString()
    {
        return $"{pizza}-{customer}";
    }
}
