using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class FoodQuestManager : MonoBehaviour
{
    public static FoodQuestManager instance;
    public List<FoodSO> foodList;
    public FoodQuestUIItem foodQuestUIItemPrefab;
    public Transform QuestSpawnPanel;
    public List<FoodQuestUIItem> foodQuestUIItems;
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
        Instantiate(foodQuestUIItemPrefab, QuestSpawnPanel);
    }
    public GameObject GetFoodPrefab(List<IngridientItem> ingridients)
    {
        int ingridientCount = ingridients.Count;
        List<FoodSO> foods = foodList
            .Where(item => item.foodReciept != null && item.foodReciept.Count == ingridientCount)
            .ToList();

        foreach (var food in foods)
        {
            int matchedIngridient = 0;
            foreach (var item in food.foodReciept)
            {
                foreach (var item2 in ingridients)
                {
                    if (item.ID == item2.foodIngridient.ID)
                    {
                        matchedIngridient++;
                    }
                }
            }
            if (matchedIngridient == ingridientCount)
            {
                return food.FoodPrefab;
            }
        }
        return null;
    }
    public void FoodDeliver(FoodSO value, bool isSucces)
    {
        foreach (var item in foodQuestUIItems)
        {
            if (item.foodSO.FoodID == value.FoodID)
            {
                foodQuestUIItems.Remove(item);
                Destroy(item.gameObject);
                break;
            }
        }

        if (isSucces) onDeliverSucces?.Invoke();
        else onDeliverFail?.Invoke();
    }
}