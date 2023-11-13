using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GardenBed : ActionObject
{
    [SerializeField] private Sprite plantSeedIcon;
    [SerializeField] private Sprite harvestIcon;
    private bool isPlantSeeded;
    private Seed seed;
    private MeshFilter sproutMesh;
    public override Type typeOfNeededItem => typeof(Seed);

    private void Awake()
    {
        OpenButton(spawnPosition, plantSeedIcon);
    }

    public override void Interact()
    {
        if (!isPlantSeeded)
            OpenInventoryWithSeeds();
        else
            Harvest();
    }

    private void Harvest()
    {
        TaskManager.Instance.CreateTask(TaskGive, this, seed.Ingredient, seed.AmountOfIngredients());
    }

    private void OpenInventoryWithSeeds()
    {
        OpenInventoryField();
    }

    public override void StartAction()
    {
        if (!isPlantSeeded)
        {
            isPlantSeeded = true;
            ItemDownCast();
            StartCoroutine(Growing());
        }
        else
        {
            isPlantSeeded = false;
            DestroyPlant();
        }
    }
    private void DestroyPlant()
    {
        Destroy(sproutMesh.gameObject);
        OpenButton(spawnPosition, plantSeedIcon);
    }

    public override void CancelAction()
    {
        if (!isPlantSeeded)
            OpenButton(spawnPosition, plantSeedIcon);
        else
            OpenButton(spawnPosition, harvestIcon);
    }

    public override void ItemDownCast()
    {
        seed = Item as Seed;
    }

    private IEnumerator Growing()
    {
        isPlantSeeded = true;
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