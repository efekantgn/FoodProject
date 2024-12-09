using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public static SaveLoad instance;
    PlayerCurrency playerCurrency;
    private IngridientProcessor[] ingridientProcessor;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        playerCurrency = FindObjectOfType<PlayerCurrency>();
        ingridientProcessor = FindObjectsOfType<IngridientProcessor>();
    }
    public void Save()
    {
        SaveData saveData = new SaveData()
        {
            Money = playerCurrency.CurrentMoney,
            FoodAndTiers = new(),
            ProcessorsAndTiers = new()
        };
        foreach (var item in FoodQuestManager.instance.ReciptList)
        {
            saveData.FoodAndTiers.Add(item, item.currentTier);
        }
        foreach (var item in ingridientProcessor)
        {
            saveData.ProcessorsAndTiers.Add(item, item.upgradeTierConfig);
        }
        SaveLoadSystem.Save("SaveLoad", saveData);
    }

}