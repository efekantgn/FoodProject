using System;
using EfeTimer;

public class Cooker : IngridientProcessor
{
    public Timer burnTimer;

    protected override void Awake()
    {
        base.Awake();
        burnTimer = new();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        OnCookComplete += StartBurnTimer;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        OnCookComplete -= StartBurnTimer;
    }

    private void StartBurnTimer()
    {
        if (ingridientItem.foodIngridientConfig is CookIngridientSO cookConfig)
        {
            burnTimer.StartTimer(cookConfig.BurnTime);
        }
    }
}