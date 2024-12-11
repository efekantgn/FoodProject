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

        if (playerCurrency.CurrentMoney < Food.currentTier.NextTier.Price)
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
        Image.sprite = Food.FoodSprite;
        if (Food.currentTier.NextTier == null)
        {
            textMesh.text = "Tier: Max";
            Price.text = "-";
        }
        else
        {
            textMesh.text = "Tier: " + Food.currentTier.NextTier.Tier.ToString();
            Price.text = "Price: " + Food.currentTier.NextTier.Price.ToString();
        }
    }
}