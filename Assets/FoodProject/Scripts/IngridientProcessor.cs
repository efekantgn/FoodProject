using System;
using EfeTimer;
using UnityEngine;
using UnityEngine.Events;

public class IngridientProcessor : MonoBehaviour
{
    [HideInInspector] public IngridientItem ingridientItem;
    [SerializeField] private ProgressCanvas processProgress;
    [SerializeField] private InteractionCanvasManager interactionCanvasManager;

    private Timer processTimer;
    public float ProcessTime = 0f;
    public Transform ItemPoint;
    public UnityEvent OnCookStart;
    public UnityEvent OnCookComplete;

    private void Awake()
    {
        processTimer = new();
        processProgress.timer = processTimer;
        processProgress.CanvasSetActive(false);
        interactionCanvasManager.ForceOpenCloseInteractionCanvas(false);
    }

    private void OnEnable()
    {
        processTimer.OnTimerStart += ProcessTimeStart;
        processTimer.OnTimerComplete += ProcessTimeComplete;
        processTimer.OnTimerUpdate += processProgress.UpdateProgressBar;

        interactionCanvasManager.Button.onClick.AddListener(SetTarget);
    }

    private void OnDisable()
    {
        processTimer.OnTimerStart -= ProcessTimeStart;
        processTimer.OnTimerComplete -= ProcessTimeComplete;
        processTimer.OnTimerUpdate -= processProgress.UpdateProgressBar;

        interactionCanvasManager.Button.onClick.RemoveListener(SetTarget);
    }
    public void StartProcessing()
    {
        processTimer.StartTimer(ProcessTime);
        interactionCanvasManager.SetIcon(ingridientItem.foodIngridient.RawSprite);
        ingridientItem.OnMoveStart.AddListener(SetIsReadyToOpen);
    }

    private void SetIsReadyToOpen()
    {
        interactionCanvasManager.IsReadyToOpen = false;
    }

    private void ProcessTimeStart()
    {
        OnCookStart?.Invoke();
        processProgress.CanvasSetActive(true);
    }
    private void ProcessTimeComplete()
    {
        OnCookComplete?.Invoke();
        interactionCanvasManager.SetIcon(ingridientItem.foodIngridient.CookedSprite);
        processProgress.CanvasSetActive(false);
        interactionCanvasManager.IsReadyToOpen = true;
    }
    private void Update()
    {
        processTimer.Tick(Time.deltaTime);
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
                    ingridientItem.OnMoveComplete.RemoveListener(StartProcessing);
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
