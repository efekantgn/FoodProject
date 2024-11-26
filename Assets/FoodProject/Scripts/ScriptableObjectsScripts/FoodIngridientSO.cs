using UnityEngine;

[CreateAssetMenu(fileName = "FoodIngridientSO", menuName = "FoodIngridientSO", order = 0)]
public class FoodIngridientSO : ScriptableObject
{
    public int ID;
    public string Name;
    public Sprite Sprite;
    public GameObject RawPrefab;
    public GameObject CookedPrefab;
    public FoodSO foodSO;

}