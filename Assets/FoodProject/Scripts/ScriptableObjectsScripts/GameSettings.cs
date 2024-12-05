using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings instance;

    public FoodUpgradeTierSO HotdogTier;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}