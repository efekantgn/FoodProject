using System;
using UnityEngine;
using UnityEngine.UI;

public class IconContainer : MonoBehaviour
{
    [SerializeField] private FoodIngridientSO foodIngridient;
    [SerializeField] private Transform PrefabSpawnTransform;
    [SerializeField] private GameObject TargetObject;

    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(ItemIconClick);
    }
    private void OnDisable()
    {
        button.onClick.RemoveListener(ItemIconClick);
    }

    private void ItemIconClick()
    {
        if (TargetObject.TryGetComponent(out IngridientCooker cooker))
        {
            IngridientItem RawItem = Instantiate(foodIngridient.RawPrefab).GetComponent<IngridientItem>();
            RawItem.transform.parent = null;
            RawItem.transform.position = PrefabSpawnTransform.position;
            RawItem.StartMovement(TargetObject.transform.position + Vector3.up);
            RawItem.OnMoveComplete.AddListener(cooker.StartCooking);
            cooker.OnCookComplete.AddListener(RawItem.DestroyThisItem);
            cooker.foodIngridient = foodIngridient;
        }
        else if (TargetObject.TryGetComponent(out Plate plate))
        {
            IngridientItem CookedItem = Instantiate(foodIngridient.CookedPrefab).GetComponent<IngridientItem>();
            CookedItem.transform.parent = null;
            CookedItem.transform.position = PrefabSpawnTransform.position;
            CookedItem.StartMovement(TargetObject.transform.position + Vector3.up);
            plate.foodConfig = foodIngridient.foodSO;
            CookedItem.OnMoveCompleteCarry.AddListener(plate.IncreaseIngridients);
        }

    }

}
