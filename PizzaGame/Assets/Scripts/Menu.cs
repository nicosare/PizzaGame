using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{    
    [SerializeField] private List<Pizza> AllPizzas;
    public List<Pizza> AvailablePizzas;
    public static Menu Instance;

    private void Awake()
    {
        Instance = this;
        
        foreach (var pizza in AllPizzas)
        {
            if (pizza.MinimalCafeRating <= RatingManager.Instance.GetRatingValue())
                AvailablePizzas.Add(pizza);
        }
    }
}