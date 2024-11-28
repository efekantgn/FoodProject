using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.VersionControl;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] public Transform TransportPoint;
    public List<IngridientItem> ingridientItems;
    public InteractionCanvasManager interactionCanvasManager;
    public GameObject spawnedFood;
    private void Start()
    {
        ingridientItems = new();
    }
    public void AddToPlate(IngridientItem ii)
    {
        ii.StartMovement(TransportPoint.position);
        ingridientItems.Add(ii);
        ii.transform.SetParent(TransportPoint);
        ii.OnMoveComplete.AddListener(SetFoodPrefab);
    }

    public bool IsPlateHasIngridient(IngridientItem ii)
    {
        foreach (var item in ingridientItems)
        {
            if (item.foodIngridient.ID == ii.foodIngridient.ID)
            {
                return true;
            }
        }
        return false;
    }

    public void SetFoodPrefab()
    {
        //food quest managera ingridientsları atıp bunlarla yapılan foodSOnun prefabını al.
        GameObject go = FoodQuestManager.instance.GetFoodPrefab(ingridientItems);
        if (spawnedFood != null) Destroy(spawnedFood);

        if (go != null)
        {
            spawnedFood = Instantiate(go, TransportPoint);
            spawnedFood.transform.position = TransportPoint.position;
            ingridientItems.ForEach(item => item.gameObject.SetActive(false));
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUp.instance.plate = this;
            interactionCanvasManager.ForceOpenCloseInteractionCanvas(true);
            interactionCanvasManager.Button.onClick.AddListener(PickUp.instance.PickUpGameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUp.instance.plate = this;
            interactionCanvasManager.ForceOpenCloseInteractionCanvas(false);
            interactionCanvasManager.Button.onClick.RemoveListener(PickUp.instance.PickUpGameObject);
        }
    }
}
