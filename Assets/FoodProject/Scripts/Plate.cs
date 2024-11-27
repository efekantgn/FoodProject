using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.VersionControl;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public FoodSO RequestFood;
    [SerializeField] public Transform TransportPoint;
    public List<IngridientItem> ingridientItems;
    public InteractionCanvasManager interactionCanvasManager;
    private void Start()
    {
        ingridientItems = new();
    }
    public void AddToPlate(IngridientItem ii)
    {
        ii.StartMovement(TransportPoint.position);
        ingridientItems.Add(ii);
        ii.transform.SetParent(TransportPoint);
    }

    public bool IsFoodContainIngridient(IngridientItem ii)
    {
        foreach (var item in RequestFood.foodReciept)
        {
            if (item.ID == ii.foodIngridient.ID)
            {
                return true;
            }
        }
        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUp p = other.transform.parent.GetComponentInChildren<PickUp>();
            p.plate = this;
            interactionCanvasManager.ForceOpenCloseInteractionCanvas(true);
            interactionCanvasManager.button.onClick.AddListener(p.PickUpGameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUp p = other.transform.parent.GetComponentInChildren<PickUp>();
            p.plate = this;
            interactionCanvasManager.ForceOpenCloseInteractionCanvas(false);
            interactionCanvasManager.button.onClick.RemoveListener(p.PickUpGameObject);
        }
    }
}
