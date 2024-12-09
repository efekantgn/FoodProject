using TMPro;
using UnityEngine;

public class ProcessorTierUpgrades : MonoBehaviour
{
    [SerializeField] private IngridientProcessor processor;
    [SerializeField] private TextMeshProUGUI textMesh;

    public void UpgradeCookerTier()
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

        textMesh.text = "Tier: " + processor.upgradeTierConfig.Tier.ToString();

    }
}