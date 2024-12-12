using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "FoodProject/New Level Settings", order = 0)]
public class LevelConfigSO : ScriptableObject
{
    [SerializeField] private string iD;
    public int Level;
    public LevelConfigSO NextLevel;
    public int CustomerCount;
    public int TargetMoneyAmount;
    public FoodSO[] LevelRecipts;

    public string ID { get => iD; private set => iD = value; }

    public void GenerateID()
    {
        ID = Guid.NewGuid().ToString();
        Debug.Log($"Generated Unique ID for {name}: {ID}");
    }
}
