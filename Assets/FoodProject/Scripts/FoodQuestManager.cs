using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FoodQuestManager : MonoBehaviour
{
    public static FoodQuestManager instance;
    public List<FoodSO> foodList;
    public FoodQuestUIItem foodQuestUIItemPrefab;
    public Transform QuestSpawnPanel;
    private List<FoodQuestUIItem> foodQuestUIItems;
    public Action<FoodSO> OnFoodRequest;
    public Action<FoodSO, bool> OnFoodDeliver;

    public UnityEvent onDeliverSucces;
    public UnityEvent onDeliverFail;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        foodQuestUIItems = new();
    }
    private void OnEnable()
    {
        OnFoodRequest += SpawnFoodQuest;
        OnFoodDeliver += FoodDeliver;
    }
    private void OnDisable()
    {
        OnFoodRequest -= SpawnFoodQuest;
        OnFoodDeliver -= FoodDeliver;
    }
    public FoodSO SelectRandomFood()
    {
        return foodList[UnityEngine.Random.Range(0, foodList.Count)];
    }
    public FoodSO RequestFood()
    {
        FoodSO f = SelectRandomFood();
        OnFoodRequest?.Invoke(f);
        return f;
    }

    public void SpawnFoodQuest(FoodSO value)
    {
        FoodQuestUIItem UIItem = Instantiate(foodQuestUIItemPrefab, QuestSpawnPanel);
        UIItem.SetText(value.FoodName);
        UIItem.foodSO = value;
        foodQuestUIItems.Add(UIItem);
    }
    public void FoodDeliver(FoodSO value, bool isSucces)
    {
        foreach (var item in foodQuestUIItems)
        {
            if (item.foodSO.FoodID == value.FoodID)
            {
                Debug.Log($"item: {item.foodSO.FoodID} - value:{value.FoodID}");

                foodQuestUIItems.Remove(item);
                Destroy(item.gameObject);
                break;
            }
        }

        if (isSucces) onDeliverSucces?.Invoke();
        else onDeliverFail?.Invoke();
    }
}