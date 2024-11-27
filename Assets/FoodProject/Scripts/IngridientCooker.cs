using System;
using EfeTimer;
using UnityEngine;
using UnityEngine.Events;

public class IngridientCooker : MonoBehaviour
{
    [HideInInspector] public IngridientItem ingridientItem;
    [SerializeField] private ProgressCanvas cookProgress;
    [SerializeField] private InteractionCanvasManager interactionCanvasManager;
    [SerializeField] private PlateSpawner plateSpawner;

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

        interactionCanvasManager.button.onClick.AddListener(SetTarget);
    }

    private void OnDisable()
    {
        cookTimer.OnTimerStart -= CookTimeStart;
        cookTimer.OnTimerComplete -= CookTimeComplete;
        cookTimer.OnTimerUpdate -= cookProgress.UpdateProgressBar;

        interactionCanvasManager.button.onClick.RemoveListener(SetTarget);
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
    public void SetTarget()
    {
        if (plateSpawner.TryGetEmptyPlate(out Plate p))
        {
            ingridientItem.StartMovement(p.TransportPoint.position);
            p.AddToPlate(ingridientItem);
        }
        else
        {
            Debug.Log("NoPlate exist");
        }

    }
}
