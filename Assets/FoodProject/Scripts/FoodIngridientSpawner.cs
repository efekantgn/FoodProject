using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FoodIngridientSpawner : MonoBehaviour
{
    public InteractionCanvasManager interactionCanvasManager;
    public FoodIngridientSO ingridientConfig;
    public Transform spawnTransform;
    public IngridientProcessor cooker;
    public UnityEvent OnIngridientSpawn;

    private void Awake()
    {
        interactionCanvasManager.CanvasSetActive(false);
        interactionCanvasManager.SetIcon(ingridientConfig.RawSprite);
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
            if (cooker.isProcessing)
            {
                Warning.instance.GiveWarning("Processor is busy.");
                return;
            }
            RawItem.OnMoveComplete.AddListener(cooker.StartProcessing);
            cooker.ingridientItem = RawItem;
            cooker.OnCookComplete += RawItem.ProcessItem;
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
                        OnIngridientSpawn?.Invoke();
                        break;
                    }
                    else
                    {
                        Warning.instance.GiveWarning("Need another Plate");
                    }
                }
            }
            else
            {
                Warning.instance.GiveWarning("No Plate exist");
            }
        }
    }
}
