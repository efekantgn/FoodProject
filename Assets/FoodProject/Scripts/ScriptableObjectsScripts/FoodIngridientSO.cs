using System;
using UnityEngine;
[CreateAssetMenu(fileName = "FoodIngridients", menuName = "FoodIngridient/ReadyToUse", order = 0)]
public class FoodIngridientSO : ScriptableObject
{
    [SerializeField] private string iD;
    public string Name;
    public Sprite RawSprite;
    public IngridientItem Prefab;

    public string ID { get => iD; private set => iD = value; }

    public void GenerateID()
    {
        ID = Guid.NewGuid().ToString();
        Debug.Log($"Generated Unique ID for {name}: {ID}");
    }
}