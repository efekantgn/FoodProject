using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public List<FoodUpgradeTierSO> foodUpgradeTiers;
    public List<FoodSO> foods;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach (var item in foods)
            {
                item.currentTier = foodUpgradeTiers[0];
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            foreach (var item in foods)
            {
                item.currentTier = foodUpgradeTiers[1];
            }
        }
    }
}