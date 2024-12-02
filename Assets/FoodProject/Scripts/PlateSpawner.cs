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
    public static PlateSpawner instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        interactionCanvasManager.CanvasSetActive(false);
    }
    private void OnEnable()
    {
        interactionCanvasManager.Button.onClick.AddListener(SpawnPlate);
    }
    private void OnDisable()
    {
        interactionCanvasManager.Button.onClick.RemoveListener(SpawnPlate);
    }
    public void SpawnPlate()
    {
        if (plates.Count > 0) return;

        Plate plate = Instantiate(platePrefab);
        plate.transform.position = plateSpawnTransform.position;
        plates.Add(plate);
    }
}
