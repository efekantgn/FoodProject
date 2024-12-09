using JetBrains.Annotations;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    PlayerCurrency playerCurrency;
    public IngridientProcessor[] ingridientProcessor;
    public FoodSO[] Foods;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        playerCurrency = FindObjectOfType<PlayerCurrency>();
    }
    public void Save()
    {
        SaveData saveData = new SaveData()
        {
            Money = playerCurrency.CurrentMoney,
            FoodAndTiers = new(),
            ProcessorsAndTiers = new()
        };
        foreach (var item in Foods)
        {
            saveData.FoodAndTiers.Add(item, item.currentTier);
            Debug.Log($"item:{item} - Tier:{item.currentTier}");
        }
        foreach (var item in ingridientProcessor)
        {
            saveData.ProcessorsAndTiers.Add(item, item.upgradeTierConfig);
            Debug.Log($"item:{item} - Tier:{item.upgradeTierConfig}");
        }

        SaveLoadSystem.Save("SaveLoad", saveData);
    }

    public void Load()
    {
        SaveData saveData = SaveLoadSystem.Load<SaveData>("SaveLoad");
        playerCurrency.CurrentMoney = saveData.Money;
        // foreach (var item in ingridientProcessor)
        // {
        //     saveData.ProcessorsAndTiers.TryGetValue(item, out item.upgradeTierConfig);
        //     Debug.Log($"item:{item} - Tier:{item.upgradeTierConfig}");

        // }
        // foreach (var item in Foods)
        // {
        //     saveData.FoodAndTiers.TryGetValue(item, out item.currentTier);
        //     Debug.Log($"item:{item} - Tier:{item.currentTier}");
        // }

    }

}