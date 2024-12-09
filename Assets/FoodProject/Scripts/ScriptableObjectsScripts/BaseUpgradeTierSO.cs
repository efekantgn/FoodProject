using UnityEngine;

public abstract class BaseUpgradeTierSO : ScriptableObject
{
    public int Tier;
    public BaseUpgradeTierSO NextTier;
    public int Price;
}
