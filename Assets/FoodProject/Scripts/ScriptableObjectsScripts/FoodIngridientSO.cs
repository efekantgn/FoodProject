using System;
using UnityEngine;
[CreateAssetMenu(fileName = "FoodIngridientSO", menuName = "FoodIngridientSO", order = 0)]
public class FoodIngridientSO : ScriptableObject
{
    public int ID;
    public string Name;
    public Sprite RawSprite;
    public Sprite CookedSprite;
    public IngridientItem Prefab;

}