using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public static AssetManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public BaseUpgradeTierSO[] upgradeTiers;
    public bool TryGetTier(string id, out BaseUpgradeTierSO tierSO)
    {
        foreach (var item in upgradeTiers)
        {
            if (item.ID == id)
            {
                tierSO = item;
                return true;
            }
        }
        tierSO = null;
        return false;
    }
}