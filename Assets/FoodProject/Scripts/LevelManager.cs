using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public LevelConfigSO LevelConfig;
    public Action OnTargetMoneyAchived;

    private PlayerCurrency playerCurrency;
    private CustomerSpawner customerSpawner;
    private bool isSucced = false;
    public UnityEvent OnLevelStart;
    public UnityEvent OnLevelFinish;
    public bool UseSave = false;

    private void Awake()
    {
        playerCurrency = FindObjectOfType<PlayerCurrency>();
        customerSpawner = FindObjectOfType<CustomerSpawner>();
    }

    private void OnEnable()
    {
        playerCurrency.OnMoneyChange += CheckTarget;
        customerSpawner.OnLastNPCLeft += LevelEnd;
    }

    private void LevelEnd()
    {
        OnLevelFinish?.Invoke();
    }

    private void OnDisable()
    {
        playerCurrency.OnMoneyChange -= CheckTarget;
        customerSpawner.OnLastNPCLeft -= LevelEnd;

    }
    private void Start()
    {
        LoadLevelData();
        StartCurrentLevel();
    }
    public void StartCurrentLevel()
    {
        //OnLevelStart?.Invoke();
        customerSpawner.SpawnNPCs(LevelConfig.CustomerCount);
        FoodQuestManager.instance.ReciptList = LevelConfig.LevelRecipts.ToList();
    }

    private void CheckTarget(int value)
    {
        if (isSucced) return;
        if (LevelConfig.TargetMoneyAmount <= value)
        {
            Warning.instance.GiveWarning("Target Achived");
            isSucced = true;
        }
    }

    [ContextMenu(nameof(StartNextLevel))]
    public void StartNextLevel()
    {
        if (LevelConfig.NextLevel == null)
        {
            Warning.instance.GiveWarning("Demo version completed.");
            return;
        }
        OnLevelStart?.Invoke();
        LevelConfig = LevelConfig.NextLevel;
        SaveLevelData();
        customerSpawner.SpawnNPCs(LevelConfig.CustomerCount);
        FoodQuestManager.instance.ReciptList = LevelConfig.LevelRecipts.ToList();
    }

    [ContextMenu("Save")]
    public void SaveLevelData()
    {
        if (!UseSave) return;
        SaveLoadSystem.Save<LevelConfigSaveData>("LevelData", new()
        {
            ID = LevelConfig.ID
        });
    }

    [ContextMenu("Load")]
    public void LoadLevelData()
    {
        if (!UseSave) return;

        if (SaveLoadSystem.TryLoad("LevelData", out LevelConfigSaveData data))
        {
            //data.ID si ile arama yap ve o idye sahip olan  
            // UpgradeTierSO yu upgradeTierConfig e ata.
            if (AssetManager.instance.TryGetLevel(data.ID, out LevelConfigSO config))
            {
                LevelConfig = config;
            }
        }
    }

    public class LevelConfigSaveData
    {
        public string ID;
    }
}
