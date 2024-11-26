using System;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] public FoodSO foodConfig;
    [SerializeField] public Transform TransportPoint;
    public List<GameObject> Ingridients = new();
    public bool isEmpty = true;

    public Canvas InteractionCanvas;

    public void IncreaseIngridients(GameObject go)
    {
        isEmpty = false;
        Ingridients.Add(go);
        PrepareItem();
    }
    public void PrepareItem()
    {
        if (foodConfig.foodIngridients.Length >= Ingridients.Count)
        {
            Instantiate(foodConfig.FoodPrefab, transform).transform.position = TransportPoint.position;
            DestroyIngridients();
        }
    }
    private void DestroyIngridients()
    {
        foreach (var item in Ingridients)
        {
            Destroy(item);
            Ingridients.Remove(item);
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
