using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProcessorTierUpgrades : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Tier;
    [SerializeField] private TextMeshProUGUI Price;
    [SerializeField] private IngridientProcessor processor;
    [SerializeField] private PlayerCurrency playerCurrency;

    private void Awake()
    {
        playerCurrency = FindObjectOfType<PlayerCurrency>();
    }
    private void Start()
    {
        UpdateUI();
    }
    public void UpgradeProcessorTier()
    {
        if (processor.upgradeTierConfig.NextTier == null)
        {
            Warning.instance.GiveWarning($"{processor.name} is last level.");
            return;
        }

        if (playerCurrency.CurrentMoney < processor.upgradeTierConfig.Price)
        {
            Warning.instance.GiveWarning($"Not enough money.");
            return;
        }

        if (processor is Cooker cooker)
            cooker.upgradeTierConfig = (CookTierSO)cooker.upgradeTierConfig.NextTier;
        else
            processor.upgradeTierConfig = (ProcessUpgradeTierSO)processor.upgradeTierConfig.NextTier;

        playerCurrency.CurrentMoney -= processor.upgradeTierConfig.Price;
        UpdateUI();
    }

    private void UpdateUI()
    {
        Tier.text = "Tier: " + processor.upgradeTierConfig.NextTier.Tier.ToString();
        Price.text = "Price: " + processor.upgradeTierConfig.NextTier.Price.ToString();
    }
}
