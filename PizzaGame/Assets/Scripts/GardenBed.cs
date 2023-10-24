using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GardenBed : ActionObject, IInteractable
{
    [SerializeField] private Sprite plantSeedIcon;
    [SerializeField] private Sprite harvestIcon;
    private bool isBusy;
    private Seed seed;
    private MeshFilter sproutMesh;
    public override Type typeOfNeededItem => typeof(Seed);
    
    private void Awake()
    {
        OpenButton(spawnPosition, plantSeedIcon);
    }

    public void Interact()
    {
        if (!isBusy)
            OpenInventoryWithSeeds();
        else
            Harvest();
    }

    private void Harvest()
    {
        Destroy(sproutMesh.gameObject);
        Give(seed.Ingredient, seed.AmountOfIngredients());
        isBusy = false;
        OpenButton(spawnPosition, plantSeedIcon);
    }

    private void OpenInventoryWithSeeds()
    {
        isBusy = true;
        OpenInventoryField();
    }

    public override void StartAction()
    {
        ItemDownCast();
        StartCoroutine(Growing());
    }

    public override void CancelAction()
    {
        isBusy = false;
        OpenButton(spawnPosition, plantSeedIcon);
    }

    public override void ItemDownCast()
    {
        seed = Item as Seed;
    }

    private IEnumerator Growing()
    {
        sproutMesh = Instantiate(seed.MeshFilters[0], transform);
        yield return new WaitForSeconds(seed.TimeToGrow / seed.MeshFilters.Length);
        for (var i = 1; i < seed.MeshFilters.Length; i++)
        {
            Destroy(sproutMesh.gameObject);
            sproutMesh = Instantiate(seed.MeshFilters[i], transform);
            yield return new WaitForSeconds(seed.TimeToGrow / seed.MeshFilters.Length);
        }

        OpenButton(spawnPosition, harvestIcon);
    }
}