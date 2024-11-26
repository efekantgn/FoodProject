using System;
using EfeTimer;
using UnityEngine;
using UnityEngine.Events;

public class IngridientCooker : MonoBehaviour
{
    [HideInInspector] public FoodIngridientSO ingridientConfig;
    [SerializeField] private ProgressCanvas cookProgress;
    [SerializeField] private InteractionCanvasManager interactionCanvasManager;

    private Timer cookTimer;
    public float CookTime = 0f;
    public Transform ItemPoint;
    public UnityEvent OnCookStart;
    public UnityEvent OnCookComplete;

    private void Awake()
    {
        cookTimer = new();
        cookProgress.ingridientCooker = this;
        interactionCanvasManager.ForceOpenCloseInteractionCanvas(false);
    }

    private void OnEnable()
    {
        cookTimer.OnTimerStart += CookTimeStart;
        cookTimer.OnTimerComplete += CookTimeComplete;
        cookTimer.OnTimerUpdate += cookProgress.UpdateProgressBar;
    }

    private void OnDisable()
    {
        cookTimer.OnTimerStart -= CookTimeStart;
        cookTimer.OnTimerComplete -= CookTimeComplete;
        cookTimer.OnTimerUpdate -= cookProgress.UpdateProgressBar;
    }
    public void StartCooking()
    {
        cookTimer.StartTimer(CookTime);
    }
    private void CookTimeStart()
    {
        OnCookStart?.Invoke();
    }
    private void CookTimeComplete()
    {
        OnCookComplete?.Invoke();
    }
    private void Update()
    {
        cookTimer.Tick(Time.deltaTime);
    }

}
