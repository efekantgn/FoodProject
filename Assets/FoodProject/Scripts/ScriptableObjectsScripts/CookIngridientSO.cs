using UnityEngine;

[CreateAssetMenu(fileName = "CookIngridient", menuName = "FoodIngridient/Cookable", order = 0)]
public class CookIngridientSO : ProcessIngridientSO
{
    public Sprite BurnedSprite;
    public float BurnTime;
}