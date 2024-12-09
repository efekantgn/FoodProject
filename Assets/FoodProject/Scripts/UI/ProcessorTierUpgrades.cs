using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProcessorTierUpgrades : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private IngridientProcessor processor;

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

        if (processor is Cooker cooker)
            cooker.upgradeTierConfig = (CookTierSO)cooker.upgradeTierConfig.NextTier;
        else
            processor.upgradeTierConfig = (ProcessUpgradeTierSO)processor.upgradeTierConfig.NextTier;

        UpdateUI();
    }

    private void UpdateUI()
    {
        textMesh.text = "Tier: " + processor.upgradeTierConfig.Tier.ToString();
    }
}
