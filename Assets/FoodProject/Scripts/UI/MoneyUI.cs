using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    PlayerCurrency playerCurrency;
    public Image Icon;
    public Canvas canvas;

    [SerializeField] TextMeshProUGUI moneyText;

    private void Awake()
    {
        playerCurrency = FindObjectOfType<PlayerCurrency>();
        canvas = GetComponentInParent<Canvas>();
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