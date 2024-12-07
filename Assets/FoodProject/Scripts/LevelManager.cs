using System;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelConfigSO LevelConfig;
    public Action OnTargetMoneyAchived;

    private PlayerCurrency playerCurrency;
    private CustomerSpawner customerSpawner;
    private bool isSucced = false;

    private void Awake()
    {
        playerCurrency = FindObjectOfType<PlayerCurrency>();
        customerSpawner = FindObjectOfType<CustomerSpawner>();
    }

    private void OnEnable()
    {
        playerCurrency.OnMoneyChange += CheckTarget;
    }

    private void OnDisable()
    {
        playerCurrency.OnMoneyChange -= CheckTarget;
    }
    private void Start()
    {
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
}
