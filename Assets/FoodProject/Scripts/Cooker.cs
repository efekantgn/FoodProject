using System;
using EfeTimer;
using UnityEngine;

public class Cooker : IngridientProcessor
{
    public Timer burnTimer;
    public ProgressCanvas burnProgressCanvas;

    protected override void Awake()
    {
        base.Awake();
        burnTimer = new();
        burnProgressCanvas.timer = burnTimer;
        burnProgressCanvas.CanvasSetActive(false);
    }

    private void StopBurning()
    {
        burnTimer.ResetTimer();
        burnProgressCanvas.CanvasSetActive(false);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        OnCookComplete += StartBurnTimer;
        burnTimer.OnTimerComplete += BurnIngridient;
        burnTimer.OnTimerUpdate += burnProgressCanvas.UpdateProgressBar;
        OnItemTakenFromProcessor += StopBurning;

    }

    private void BurnIngridient()
    {
        ingridientItem.Burned.SetActive(true);
        ingridientItem.Cooked.SetActive(false);
        ingridientItem.foodState = FoodState.Burned;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnCookComplete -= StartBurnTimer;
        burnTimer.OnTimerComplete -= BurnIngridient;
        burnTimer.OnTimerUpdate -= burnProgressCanvas.UpdateProgressBar;
        OnItemTakenFromProcessor -= StopBurning;
    }

    private void StartBurnTimer()
    {
        if (ingridientItem.foodIngridientConfig is CookIngridientSO cookConfig)
        {
            if (upgradeTierConfig is CookTierSO cookTierconfig)
            {
                burnTimer.StartTimer(cookConfig.BurnTime * cookTierconfig.BurnMultiplier);
                burnProgressCanvas.CanvasSetActive(true);
            }
        }
    }
    protected override void Update()
    {
        base.Update();
        burnTimer.Tick(Time.deltaTime);
    }
}