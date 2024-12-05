using System;
using EfeTimer;
using UnityEngine;

public class IngridientProcessor : MonoBehaviour
{
    [HideInInspector] public IngridientItem ingridientItem;
    [SerializeField] private ProgressCanvas processProgress;
    [SerializeField] private InteractionCanvasManager interactionCanvasManager;

    protected Timer processTimer;
    public bool isProcessing = false;
    public Transform ItemPoint;
    public Action OnCookComplete;

    protected virtual void Awake()
    {
        processTimer = new();
        processProgress.timer = processTimer;
        processProgress.CanvasSetActive(false);
        interactionCanvasManager.ForceOpenCloseInteractionCanvas(false);
    }

    protected virtual void OnEnable()
    {
        processTimer.OnTimerStart += ProcessTimeStart;
        processTimer.OnTimerComplete += ProcessTimeComplete;
        processTimer.OnTimerUpdate += processProgress.UpdateProgressBar;

        interactionCanvasManager.Button.onClick.AddListener(SetTarget);

    }

    protected virtual void OnDisable()
    {
        processTimer.OnTimerStart -= ProcessTimeStart;
        processTimer.OnTimerComplete -= ProcessTimeComplete;
        processTimer.OnTimerUpdate -= processProgress.UpdateProgressBar;

        interactionCanvasManager.Button.onClick.RemoveListener(SetTarget);
    }
    public void StartProcessing()
    {
        if (ingridientItem.foodIngridientConfig is ProcessIngridientSO processIngridientConfig)
        {
            isProcessing = true;
            processTimer.StartTimer(processIngridientConfig.ProcessTime);
            interactionCanvasManager.SetIcon(processIngridientConfig.RawSprite);
            ingridientItem.OnMoveStart.AddListener(SetIsReadyToOpen);
        }
    }

    private void SetIsReadyToOpen()
    {
        interactionCanvasManager.IsReadyToOpen = false;
    }

    private void ProcessTimeStart()
    {
        processProgress.CanvasSetActive(true);
    }
    private void ProcessTimeComplete()
    {
        if (ingridientItem.foodIngridientConfig is ProcessIngridientSO processIngridient)
        {
            Debug.Log($"processIngidient:{processIngridient.Name}");
            OnCookComplete?.Invoke();
            interactionCanvasManager.SetIcon(processIngridient.ProcessedSprite);
            processProgress.CanvasSetActive(false);
            interactionCanvasManager.IsReadyToOpen = true;
        }
    }
    private void Update()
    {
        processTimer.Tick(Time.deltaTime);
    }
    public void SetTarget()
    {
        OnCookComplete -= ingridientItem.ProcessItem;
        if (PlateSpawner.instance.plates.Count > 0)
        {
            foreach (var p in PlateSpawner.instance.plates)
            {
                if (!p.IsPlateHasIngridient(ingridientItem))
                {
                    p.AddToPlate(ingridientItem);
                    ingridientItem.OnMoveComplete.RemoveListener(StartProcessing);
                    ingridientItem.OnMoveStart.RemoveListener(SetIsReadyToOpen);
                    isProcessing = false;
                    break;
                }
                else
                {
                    Warning.instance.GiveWarning("Already added to plate.");
                }
            }
        }
        else
        {
            Warning.instance.GiveWarning("No Plate exist");
        }
    }
}
