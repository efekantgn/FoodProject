using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plate : MonoBehaviour
{
    public Transform TransportPoint;
    public List<IngridientItem> ingridientItems;
    public InteractionCanvasManager interactionCanvasManager;
    public GameObject spawnedFood;
    public Image FoodImage;
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
            if (item.foodIngridientConfig.ID == ii.foodIngridientConfig.ID)
            {
                return true;
            }
        }
        return false;
    }

    public void SetFoodPrefab()
    {
        //food quest managera ingridientsları atıp bunlarla yapılan foodSOnun prefabını al.
        if (spawnedFood != null) Destroy(spawnedFood);
        FoodSO foodSO = FoodQuestManager.instance.GetFoodPrefab(ingridientItems);

        if (foodSO != null)
        {
            spawnedFood = Instantiate(foodSO.FoodPrefab, TransportPoint);
            spawnedFood.transform.position = TransportPoint.position;
            FoodImage.enabled = true;
            FoodImage.sprite = foodSO.FoodSprite;
            ingridientItems.ForEach(item => item.gameObject.SetActive(false));
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionCanvasManager.ForceOpenCloseInteractionCanvas(true);
            interactionCanvasManager.Button.onClick.AddListener(PickUpSetPlate);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionCanvasManager.ForceOpenCloseInteractionCanvas(false);
            interactionCanvasManager.Button.onClick.RemoveListener(PickUpSetPlate);
        }
    }
    public void PickUpSetPlate()
    {
        PickUp.instance.PickUpGameObject(this);
    }
}
