using System;
using TMPro;
using UnityEngine;

public class FoodQuestUIItem : MonoBehaviour
{
    public FoodSO foodSO;
    private TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        FoodQuestManager.instance.OnFoodRequest += SetUI;
    }

    private void SetUI(FoodSO sO)
    {
        foodSO = sO;
        SetText(sO.FoodName);
        FoodQuestManager.instance.foodQuestUIItems.Add(this);
    }

    private void OnDisable()
    {
        FoodQuestManager.instance.OnFoodRequest -= SetUI;
    }
    public void SetText(string value)
    {
        textMeshProUGUI.text = value;
    }
}