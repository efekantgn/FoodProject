using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodIngridientSpawner : MonoBehaviour
{
    public InteractionCanvasManager interactionCanvasManager;
    public FoodIngridientSO ingridientConfig;
    public Transform spawnTransform;
    public IngridientCooker cooker;

    private Button button;

    private void Awake()
    {
        button = GetComponentInChildren<Button>();
        interactionCanvasManager.CanvasSetActive(false);
    }
    private void OnEnable()
    {
        button.onClick.AddListener(SpawnFood);
    }
    private void OnDisable()
    {
        button.onClick.RemoveListener(SpawnFood);
    }

    public void SpawnFood()
    {
        IngridientItem RawItem = Instantiate(ingridientConfig.Prefab).GetComponent<IngridientItem>();
        RawItem.transform.parent = null;
        RawItem.transform.position = spawnTransform.position;
        if (RawItem.foodState == FoodState.Raw)
        {
            RawItem.OnMoveComplete.AddListener(cooker.StartCooking);
            cooker.ingridientConfig = ingridientConfig;
            cooker.OnCookComplete.AddListener(RawItem.CookItem);
            RawItem.StartMovement(cooker.ItemPoint.position);
        }
        if (RawItem.foodState == FoodState.ReadyToUse || RawItem.foodState == FoodState.Cooked)
        {
            //Go To Plate
        }


    }
}
