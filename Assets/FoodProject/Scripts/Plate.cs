using System;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] public FoodSO foodConfig;
    [SerializeField] public Transform TransportPoint;
    public List<GameObject> Ingridients = new();


    public void IncreaseIngridients(GameObject go)
    {
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
}
