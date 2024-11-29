using System;
using EfeTimer;
using UnityEngine;
using UnityEngine.Events;

public class IngridientCooker : MonoBehaviour
{
    [HideInInspector] public IngridientItem ingridientItem;
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
        cookProgress.timer = cookTimer;
        cookProgress.CanvasSetActive(false);
        interactionCanvasManager.ForceOpenCloseInteractionCanvas(false);
    }

    private void OnEnable()
    {
        cookTimer.OnTimerStart += CookTimeStart;
        cookTimer.OnTimerComplete += CookTimeComplete;
        cookTimer.OnTimerUpdate += cookProgress.UpdateProgressBar;

        interactionCanvasManager.Button.onClick.AddListener(SetTarget);
    }

    private void OnDisable()
    {
        cookTimer.OnTimerStart -= CookTimeStart;
        cookTimer.OnTimerComplete -= CookTimeComplete;
        cookTimer.OnTimerUpdate -= cookProgress.UpdateProgressBar;

        interactionCanvasManager.Button.onClick.RemoveListener(SetTarget);
    }
    public void StartCooking()
    {
        cookTimer.StartTimer(CookTime);
        interactionCanvasManager.SetIcon(ingridientItem.foodIngridient.RawSprite);
        ingridientItem.OnMoveStart.AddListener(SetIsReadyToOpen);
    }

    private void SetIsReadyToOpen()
    {
        interactionCanvasManager.IsReadyToOpen = false;
    }

    private void CookTimeStart()
    {
        OnCookStart?.Invoke();
        cookProgress.CanvasSetActive(true);
    }
    private void CookTimeComplete()
    {
        OnCookComplete?.Invoke();
        interactionCanvasManager.SetIcon(ingridientItem.foodIngridient.CookedSprite);
        cookProgress.CanvasSetActive(false);
        interactionCanvasManager.IsReadyToOpen = true;
    }
    private void Update()
    {
        cookTimer.Tick(Time.deltaTime);
    }
    public void SetTarget()
    {
        OnCookComplete.RemoveListener(ingridientItem.CookItem);
        if (PlateSpawner.instance.plates.Count > 0)
        {
            foreach (var p in PlateSpawner.instance.plates)
            {
                if (!p.IsPlateHasIngridient(ingridientItem))
                {
                    p.AddToPlate(ingridientItem);
                    ingridientItem.OnMoveComplete.RemoveListener(StartCooking);
                    ingridientItem.OnMoveStart.RemoveListener(SetIsReadyToOpen);
                    break;
                }
                else
                {
                    Debug.Log("Need another Plate");
                }
            }
        }
        else
        {
            Debug.Log("NoPlate exist");
        }
    }
}
