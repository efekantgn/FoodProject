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

    private void Awake()
    {
        interactionCanvasManager.CanvasSetActive(false);
        interactionCanvasManager.SetIcon(ingridientConfig.Sprite);

    }
    private void OnEnable()
    {
        interactionCanvasManager.Button.onClick.AddListener(SpawnFood);

    }
    private void OnDisable()
    {
        interactionCanvasManager.Button.onClick.RemoveListener(SpawnFood);
    }

    public void SpawnFood()
    {
        IngridientItem RawItem = Instantiate(ingridientConfig.Prefab);
        RawItem.transform.parent = null;
        RawItem.transform.position = spawnTransform.position;
        if (RawItem.foodState == FoodState.Raw)
        {
            RawItem.OnMoveComplete.AddListener(cooker.StartCooking);
            cooker.ingridientItem = RawItem;
            cooker.OnCookComplete.AddListener(RawItem.CookItem);
            RawItem.StartMovement(cooker.ItemPoint.position);
        }
        if (RawItem.foodState == FoodState.ReadyToUse || RawItem.foodState == FoodState.Cooked)
        {
            //Go To Plate
            if (PlateSpawner.instance.plates.Count > 0)
            {
                foreach (var p in PlateSpawner.instance.plates)
                {
                    if (!p.IsPlateHasIngridient(RawItem))
                    {
                        p.AddToPlate(RawItem);
                        break;
                    }
                    else
                    {
                        Debug.Log("Need another Plate");
                    }
                }
            }
            else
            {
                Debug.Log("NoPlate exist");
            }
        }


    }

}
