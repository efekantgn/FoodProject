using UnityEngine;

[CreateAssetMenu(fileName = "CookIngridient", menuName = "FoodProject/FoodIngridient/New Cookable", order = 0)]
public class CookIngridientSO : ProcessIngridientSO
{
    public Sprite BurnedSprite;
    public float BurnTime;
}