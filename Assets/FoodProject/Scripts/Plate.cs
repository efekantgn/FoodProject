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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUp.instance.plate = this;
            interactionCanvasManager.ForceOpenCloseInteractionCanvas(true);
            interactionCanvasManager.button.onClick.AddListener(PickUp.instance.PickUpGameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUp.instance.plate = this;
            interactionCanvasManager.ForceOpenCloseInteractionCanvas(false);
            interactionCanvasManager.button.onClick.RemoveListener(PickUp.instance.PickUpGameObject);
        }
    }
}
