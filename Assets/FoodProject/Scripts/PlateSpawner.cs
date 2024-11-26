using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateSpawner : MonoBehaviour
{
    public InteractionCanvasManager interactionCanvasManager;
    public Plate platePrefab;
    public Transform plateSpawnTransform;
    public List<Plate> plates = new();

    private void Start()
    {
        interactionCanvasManager.CanvasSetActive(false);
    }
    public void SpawnPlate()
    {
        if (GetEmptyPlate(out Plate p)) return;

        Plate plate = Instantiate(platePrefab);
        plate.transform.position = plateSpawnTransform.position;
        plates.Add(plate);
    }

    public bool GetEmptyPlate(out Plate p)
    {
        foreach (var item in plates)
        {
            if (item.isEmpty)
            {
                p = item;
                return true;
            }
        }
        p = null;
        return false;
    }
}
