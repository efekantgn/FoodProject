using UnityEngine;

[CreateAssetMenu(fileName = "FoodIngridientSO", menuName = "FoodIngridientSO", order = 0)]
public class FoodIngridientSO : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public GameObject Prefab;

}