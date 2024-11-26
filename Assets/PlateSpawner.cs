using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateSpawner : MonoBehaviour
{
    public static PlateSpawner Instance;
    private Button button;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        button = GetComponent<Button>();
    }
    public Plate platePrefab;
    public Transform plateSpawnTransform;
    public List<Plate> plates = new();

    private void OnEnable()
    {
        button.onClick.AddListener(SpawnPlate);
    }
    private void OnDisable()
    {
        button.onClick.RemoveListener(SpawnPlate);
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
