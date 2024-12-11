using System;
using TMPro;
using UnityEngine;

public class PlayerCurrency : MonoBehaviour
{
    private int currentMoney = 0;

    /// <summary>
    /// parametre olarak CurrentMoney alıyor.
    /// </summary>
    public Action<int> OnMoneyChange;
    public int CurrentMoney
    {
        get => currentMoney; set
        {
            currentMoney = value;
            OnMoneyChange?.Invoke(currentMoney);
        }
    }

    private void OnEnable()
    {
        OnMoneyChange += SaveMoney;
    }
    private void OnDisable()
    {
        OnMoneyChange -= SaveMoney;
    }
    private void Start()
    {
        LoadMoney();
    }
    public void SaveMoney(int value)
    {
        PlayerCurrencyData data = new()
        {
            Money = value
        };
        SaveLoadSystem.Save("PlayerCurrencyData", data);
    }

    public void LoadMoney()
    {
        PlayerCurrencyData data = SaveLoadSystem.Load<PlayerCurrencyData>("PlayerCurrencyData");
        CurrentMoney = data.Money;
    }
    public struct PlayerCurrencyData
    {
        public int Money;
    }
}