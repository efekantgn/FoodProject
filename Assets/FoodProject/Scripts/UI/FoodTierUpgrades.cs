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
        LoadFoodData();
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
        playerCurrency.CurrentMoney -= Food.currentTier.Price;
        UpdateUI();
        SaveFoodData();
    }

    [ContextMenu("Save")]
    public void SaveFoodData()
    {
        SaveLoadSystem.Save<FoodTierSaveData>(Food.ID, new()
        {
            ID = Food.currentTier.ID
        });
    }

    [ContextMenu("Load")]
    public void LoadFoodData()
    {
        if (SaveLoadSystem.TryLoad(Food.ID, out FoodTierSaveData data))
        {
            //data.ID si ile arama yap ve o idye sahip olan  
            // UpgradeTierSO yu upgradeTierConfig e ata.
            if (AssetManager.instance.TryGetTier(data.ID, out BaseUpgradeTierSO tier))
            {
                Food.currentTier = (FoodUpgradeTierSO)tier;
            }
        }
    }

    private void UpdateUI()
    {
        Image.sprite = Food.FoodSprite;
        if (Food.currentTier.NextTier == null)
        {
            textMesh.text = "Maxed";
            Price.text = "-";
        }
        else
        {
            textMesh.text = "Tier: " + Food.currentTier.NextTier.Tier.ToString();
            Price.text = "Tier: " + Food.currentTier.NextTier.Price.ToString();
        }
    }
    public class FoodTierSaveData
    {
        public string ID;
    }
}