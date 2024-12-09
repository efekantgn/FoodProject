using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodTierUpgrades : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private TextMeshProUGUI Price;

    [SerializeField] private Image Image;
    [SerializeField] private FoodSO Food;
    [SerializeField] private PlayerCurrency playerCurrency;

    private void Awake()
    {
        playerCurrency = FindObjectOfType<PlayerCurrency>();
    }
    private void Start()
    {
        UpdateUI();
    }
    public void UpgradeFoodTier()
    {
        if (Food.currentTier.NextTier == null)
        {
            Warning.instance.GiveWarning($"{Food.FoodName} is Max level.");
            return;
        }

        if (playerCurrency.CurrentMoney < Food.FoodPrice)
        {
            Warning.instance.GiveWarning($"Not enough money.");
            return;
        }

        Food.currentTier = (FoodUpgradeTierSO)Food.currentTier.NextTier;
        playerCurrency.CurrentMoney -= Food.FoodPrice;
        UpdateUI();
    }

    private void UpdateUI()
    {
        textMesh.text = "Tier: " + Food.currentTier.NextTier.Tier.ToString();
        Price.text = "Tier: " + Food.currentTier.NextTier.Price.ToString();
        Image.sprite = Food.FoodSprite;
    }
}