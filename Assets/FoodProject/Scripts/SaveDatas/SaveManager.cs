using JetBrains.Annotations;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    PlayerCurrency playerCurrency;
    public IngridientProcessor[] ingridientProcessor;
    public FoodSO[] Foods;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        playerCurrency = FindObjectOfType<PlayerCurrency>();
    }
}