using System;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] public Transform TransportPoint;
    public List<IngridientItem> ingridientItems;
    public bool isEmpty = true;

    public InteractionCanvasManager interactionCanvasManager;
    private void Start()
    {
        ingridientItems = new();
    }
    public void AddToPlate(IngridientItem ii)
    {
        isEmpty = false;
        ingridientItems.Add(ii);
        ii.transform.SetParent(TransportPoint);
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
