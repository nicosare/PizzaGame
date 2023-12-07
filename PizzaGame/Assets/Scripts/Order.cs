using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order
{
    public Pizza Pizza;
    public Customer Customer;
    public float Cost;
    public float Rating;
    public int PizzaLevel;

    public Order()
    {
        Pizza = null;
        Customer = null;
    }

    public Order(Order order)
    {
        Pizza = order.Pizza;
        Customer = order.Customer;
        Cost = order.Cost;
        Rating = order.Rating;
        PizzaLevel = order.PizzaLevel;
    }

    public Order(Pizza pizza, Customer customer, float cost, float rating, int pizzaLevel)
    {
        Pizza = pizza;
        Customer = customer;
        Cost = cost;
        Rating = rating;
        PizzaLevel = pizzaLevel;
    }

    public override string ToString()
    {
        return $"{Pizza}-{Customer}";
    }
}
