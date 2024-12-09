using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodTierUpgrades : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Image Image;
    [SerializeField] private FoodSO Food;

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

        Food.currentTier = (FoodUpgradeTierSO)Food.currentTier.NextTier;
        UpdateUI();
    }

    private void UpdateUI()
    {
        textMesh.text = "Tier: " + Food.currentTier.Tier.ToString();
        Image.sprite = Food.FoodSprite;
    }
}