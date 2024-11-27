using TMPro;
using UnityEngine;

public class FoodQuestUIItem : MonoBehaviour
{
    public FoodSO foodSO;
    private TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetText(string value)
    {
        textMeshProUGUI.text = value;
    }
}