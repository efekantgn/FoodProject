using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "FoodProject/New Food", order = 0)]
public class FoodSO : ScriptableObject
{
    [SerializeField] private string iD;
    public string FoodName;
    public int FoodPrice;
    public Sprite FoodSprite;
    public GameObject FoodPrefab;
    public FoodUpgradeTierSO currentTier;
    public List<FoodIngridientSO> foodReciept;
    public string ID { get => iD; private set => iD = value; }

    public void GenerateID()
    {
        ID = Guid.NewGuid().ToString();
        Debug.Log($"Generated Unique ID for {name}: {ID}");
    }
}