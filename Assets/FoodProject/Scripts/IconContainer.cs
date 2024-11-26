using System;
using UnityEngine;
using UnityEngine.UI;

public class IconContainer : MonoBehaviour
{
    [SerializeField] private FoodIngridientSO foodIngridient;
    [SerializeField] private Transform PrefabSpawnTransform;
    [SerializeField] private GameObject Cooker;

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
        IngridientItem ii = Instantiate(foodIngridient.RawPrefab).GetComponent<IngridientItem>();
        ii.transform.parent = null;
        ii.transform.position = PrefabSpawnTransform.position;
        IngridientCooker cooker = Cooker.GetComponent<IngridientCooker>();
        ii.StartMovement(cooker.TransportTarget);
        ii.OnMoveComplete.AddListener(cooker.StartCooking);
        cooker.OnCookComplete.AddListener(ii.DestroyThisItem);
        cooker.foodIngridient = foodIngridient;
    }
}
