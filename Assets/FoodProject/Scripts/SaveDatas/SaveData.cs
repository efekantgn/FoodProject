using System.Collections.Generic;

struct SaveData
{
    public int Money;
    public Dictionary<FoodSO, FoodUpgradeTierSO> FoodAndTiers;
    public Dictionary<IngridientProcessor, ProcessUpgradeTierSO> ProcessorsAndTiers;
}