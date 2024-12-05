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
}