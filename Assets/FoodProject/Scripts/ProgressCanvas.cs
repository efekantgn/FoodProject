using System;
using EfeTimer;
using UnityEngine;
using UnityEngine.UI;

public class ProgressCanvas : MonoBehaviour
{
    [SerializeField] private Image progres;
    public GameObject GeneralPanel;
    public Timer timer;

    public void UpdateProgressBar(float value)
    {
        progres.fillAmount = value / timer.Duration;
    }
    public void CanvasSetActive(bool b)
    {
        GeneralPanel.SetActive(b);
    }
}