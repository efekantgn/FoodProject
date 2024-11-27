using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Food", order = 0)]
public class FoodSO : ScriptableObject
{
    public int FoodID;
    public string FoodName;
    public Sprite FoodSprite;
    public GameObject FoodPrefab;
    public List<FoodIngridientSO> foodReciept;
}