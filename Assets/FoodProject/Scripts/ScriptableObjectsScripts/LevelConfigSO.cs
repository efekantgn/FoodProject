using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "FoodProject/New Level Settings", order = 0)]
public class LevelConfigSO : ScriptableObject
{
    public int Level;
    public LevelConfigSO NextLevel;
    public int CustomerCount;
    public int TargetMoneyAmount;
    public FoodSO[] LevelRecipts;
}