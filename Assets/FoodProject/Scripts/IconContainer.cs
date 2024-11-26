using System;
using UnityEngine;
using UnityEngine.UI;

public class IconContainer : MonoBehaviour
{
    [SerializeField] private FoodIngridientSO foodIngridient;
    [SerializeField] private Transform PrefabSpawnTransform;

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
        Instantiate(foodIngridient.Prefab, PrefabSpawnTransform);
    }
}
