using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{    
    [SerializeField] private List<Pizza> AllPizzas;
    public List<Pizza> AvailablePizzas;
    public static Menu Instance;

    //TODO: Перенести рейтинг в скрипт кафе
    [SerializeField] private float rating;

    private void Awake()
    {
        Instance = this;
        
        foreach (var pizza in AllPizzas)
        {
            if (pizza.minimalCafeRating <= rating)
                AvailablePizzas.Add(pizza);
        }
    }
}