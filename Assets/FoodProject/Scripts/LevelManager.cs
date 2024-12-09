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
        StartNextLevel();
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

    public void StartNextLevel()
    {
        OnLevelStart?.Invoke();
        LevelConfig = LevelConfig.NextLevel;
        customerSpawner.SpawnNPCs(LevelConfig.CustomerCount);
        FoodQuestManager.instance.ReciptList = LevelConfig.LevelRecipts.ToList();
    }

}
