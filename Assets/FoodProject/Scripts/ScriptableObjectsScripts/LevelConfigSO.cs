using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfigSO", menuName = "LevelConfigSO", order = 0)]
public class LevelConfigSO : ScriptableObject
{
    public int Level;
    public int CustomerCount;
    /// <summary>
    /// Masa sistemi statik hale geldi
    /// </summary>
    /// //public int TableCount;
    public int TargetMoneyAmount;
    public FoodSO[] LevelRecipts;
}