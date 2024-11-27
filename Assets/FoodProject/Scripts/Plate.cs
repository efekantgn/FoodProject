using System;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] public FoodSO foodConfig;
    [SerializeField] public Transform TransportPoint;
    public List<IngridientItem> ingridientItems;
    public bool isEmpty = true;

    public Canvas InteractionCanvas;
    private void Start()
    {
        ingridientItems = new();
    }
    public void AddToPlate(IngridientItem ii)
    {
        isEmpty = false;
        ingridientItems.Add(ii);
        PrepareItem();
    }
    public void PrepareItem()
    {
        if (foodConfig.foodIngridients.Length >= ingridientItems.Count)
        {
            Instantiate(foodConfig.FoodPrefab, transform).transform.position = TransportPoint.position;
            DestroyIngridients();
        }
    }
    private void DestroyIngridients()
    {
        foreach (var item in ingridientItems)
        {
            Destroy(item);
            ingridientItems.Remove(item);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractionCanvas.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteractionCanvas.gameObject.SetActive(false);
        }
    }
}
