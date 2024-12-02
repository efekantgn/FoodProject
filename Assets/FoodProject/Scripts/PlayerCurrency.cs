using System;
using TMPro;
using UnityEngine;

public class PlayerCurrency : MonoBehaviour
{
    private int moneyAmount = 0;
    public Action<int> OnMoneyChange;
    public TextMeshProUGUI textMesh;

    public int MoneyAmount
    {
        get => moneyAmount; set
        {
            moneyAmount = value;
            OnMoneyChange?.Invoke(moneyAmount);
        }
    }
    private void OnEnable()
    {
        OnMoneyChange += UpdateUI;
    }
    private void OnDisable()
    {
        OnMoneyChange -= UpdateUI;
    }

    private void Start()
    {
        UpdateUI(moneyAmount);
    }
    public void UpdateUI(int value)
    {
        textMesh.text = value.ToString();
    }
}