using UnityEngine;

[CreateAssetMenu(fileName = "ProcessIngridient", menuName = "FoodIngridient/Processable", order = 0)]
public class ProcessIngridientSO : FoodIngridientSO
{
    public Sprite ProcessedSprite;
    public float ProcessTime;

}
