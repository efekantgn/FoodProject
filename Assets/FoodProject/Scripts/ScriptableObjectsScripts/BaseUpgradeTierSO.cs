using UnityEngine;
using System;

public abstract class BaseUpgradeTierSO : ScriptableObject
{
    [SerializeField] private string iD;
    public int Tier;
    public BaseUpgradeTierSO NextTier;
    public int Price;
    public string ID { get => iD; private set => iD = value; }

    public void GenerateID()
    {
        ID = Guid.NewGuid().ToString();
        Debug.Log($"Generated Unique ID for {name}: {ID}");
    }
}
