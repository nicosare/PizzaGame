using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBed : ActionObject, IInteractable
{
    [SerializeField] private Seed seed;
    [SerializeField] private Sprite plantSeedIcon;
    [SerializeField] private Sprite harvestIcon;
    private bool isBusy;

    private void Awake()
    {
        OpenButton(spawnPosition, plantSeedIcon);
    }

    private void Start()
    {
        Give(seed, 2);
    }

    public void Interact()
    {
        if (!isBusy)
            PlantSeed();
        else
            Harvest();
    }

    private void Harvest()
    {
        Give(seed.Ingredient, seed.AmountOfIngredients());
        isBusy = false;
        OpenButton(spawnPosition, plantSeedIcon);
    }

    private void PlantSeed()
    {
        Take(seed);
        isBusy = true;
        StartCoroutine(Growing());
    }

    private IEnumerator Growing()
    {
        yield return new WaitForSeconds(seed.TimeToGrow);
        OpenButton(spawnPosition, harvestIcon);
    }
}