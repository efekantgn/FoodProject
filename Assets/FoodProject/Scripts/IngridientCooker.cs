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
        cookProgress.ingridientCooker = this;
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
        interactionCanvasManager.SetIcon(ingridientItem.foodIngridient.Sprite);
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
        ingridientItem.OnMoveComplete.RemoveListener(StartCooking);
        OnCookComplete.RemoveListener(ingridientItem.CookItem);
        if (PlateSpawner.instance.plates.Count > 0)
        {
            foreach (var p in PlateSpawner.instance.plates)
            {
                if (!p.IsPlateHasIngridient(ingridientItem))
                {
                    p.AddToPlate(ingridientItem);
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
