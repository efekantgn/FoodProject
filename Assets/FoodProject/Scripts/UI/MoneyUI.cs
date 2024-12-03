using System;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    PlayerCurrency playerCurrency;

    [SerializeField] TextMeshProUGUI moneyText;

    private void Awake()
    {
        playerCurrency = FindObjectOfType<PlayerCurrency>();
    }

    private void OnEnable()
    {
        playerCurrency.OnMoneyChange += UpdateUI;
    }
    private void Start()
    {
        UpdateUI(playerCurrency.CurrentMoney);
    }

    private void UpdateUI(int curretMoney)
    {
        moneyText.text = curretMoney.ToString();
    }

    private void OnDisable()
    {
        playerCurrency.OnMoneyChange -= UpdateUI;
    }
}