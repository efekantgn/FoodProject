using System;
using EfeTimer;
using UnityEngine;
using UnityEngine.Events;

public class IngridientCooker : MonoBehaviour
{
    public FoodIngridientSO foodIngridient;
    private Timer cookTimer;
    [SerializeField] private ProgressCanvas cookingCanvas;

    public float CookTime = 0f;
    public Transform TransportTarget;
    public UnityEvent OnCookStart;
    public UnityEvent OnCookComplete;

    private void Awake()
    {
        cookTimer = new();
        cookingCanvas.ingridientCooker = this;
    }

    private void OnEnable()
    {
        cookTimer.OnTimerStart += CookTimeStart;
        cookTimer.OnTimerComplete += CookTimeComplete;
        cookTimer.OnTimerUpdate += cookingCanvas.UpdateProgressBar;
    }

    private void OnDisable()
    {
        cookTimer.OnTimerStart -= CookTimeStart;
        cookTimer.OnTimerComplete -= CookTimeComplete;
        cookTimer.OnTimerUpdate -= cookingCanvas.UpdateProgressBar;
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
