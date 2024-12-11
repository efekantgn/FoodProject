using System;
using System.Collections.Generic;
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
        OnMoneyChange += (i) => Handheld.Vibrate();
    }
    private void Start()
    {
        LoadMoney();
    }
    private void OnDisable()
    {
        OnMoneyChange -= SaveMoney;
    }
    private void SaveMoney(int value)
    {
        SaveLoadSystem.Save<Currency>("Money", new()
        {
            Money = value
        });
    }
    private void LoadMoney()
    {
        if (SaveLoadSystem.TryLoad("Money", out Currency c))
        {
            if (EqualityComparer<Currency>.Default.Equals(c, default)) return;

            CurrentMoney = c.Money;
        }
    }

    public struct Currency
    {
        public int Money;
    }
}
