using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressCanvas : MonoBehaviour
{
    [SerializeField] private Image progres;
    public IngridientCooker ingridientCooker;

    public void UpdateProgressBar(float value)
    {
        Debug.Log($"timer: {value}");
        progres.fillAmount = value / ingridientCooker.CookTime;
    }
}