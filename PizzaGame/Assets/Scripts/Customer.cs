using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Customer : MonoBehaviour
{
    public List<Pizza> DesiredPizzas;

    private void Start()
    {
        GetDesiredPizza();
    }

    private void GetDesiredPizza()
    {
        var availableMenu = Menu.Instance.AvailablePizzas.Shuffle();
        
        for (var i = 0; i < 2; i++)
        {
            DesiredPizzas.Add(availableMenu[i]);
            if (availableMenu.Count < 2)
                break;
        }
    }
}