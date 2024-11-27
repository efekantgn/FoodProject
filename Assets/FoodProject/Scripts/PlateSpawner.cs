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
        if (TryGetEmptyPlate(out Plate p)) return;

        Plate plate = Instantiate(platePrefab);
        plate.transform.position = plateSpawnTransform.position;
        plates.Add(plate);
    }

    public bool TryGetEmptyPlate(out Plate p)
    {
        foreach (var item in plates)
        {

            p = item;
            return true;

        }
        p = null;
        return false;
    }
}
