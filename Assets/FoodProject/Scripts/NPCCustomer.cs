using System;
using EfeTimer;
using UnityEngine;

public class NPCCustomer : MonoBehaviour
{
    private FoodSO orderConfig;
    private InteractionCanvasManager interactionCanvasManager;
    private NPCMovement movement;
    public Timer timer;
    private float remainingTime = 0;
    [SerializeField] private float customerStayTimeInSecond;
    private CharModelSelector charModel;
    private bool isYelled = false;
    public Money Money;


    private void Awake()
    {
        interactionCanvasManager = GetComponentInChildren<InteractionCanvasManager>();
        movement = GetComponentInChildren<NPCMovement>();
        charModel = GetComponentInChildren<CharModelSelector>();

        timer = new Timer();

    }
    private void OnEnable()
    {
        interactionCanvasManager.Button.onClick.AddListener(TakeFood);
        interactionCanvasManager.Button.onClick.AddListener(PickUpEnd);

        //Timer Actions
        timer.OnTimerComplete += TimerComplete;
        timer.OnTimerUpdate += TimerUpdate;
        timer.OnTimerReset += TimerReset;

        movement.OnPlayerStandUp += PlayerStandUp;


    }

    private void TimerReset()
    {
        interactionCanvasManager.gameObject.SetActive(false);
    }

    private void TimerUpdate(float value)
    {
        remainingTime = value;
        if (!isYelled && value < timer.Duration / 2)
        {
            charModel.YellTimer();
            isYelled = true;
        }
    }

    private void OnDisable()
    {
        interactionCanvasManager.Button.onClick.RemoveListener(TakeFood);
        interactionCanvasManager.Button.onClick.RemoveListener(PickUpEnd);

        //Timer Actions
        timer.OnTimerComplete -= TimerComplete;
        timer.OnTimerReset -= TimerReset;
        timer.OnTimerUpdate -= TimerUpdate;
        movement.OnPlayerStandUp -= PlayerStandUp;


    }

    private void PlayerStandUp()
    {
        //TODO: süre bitince değil ayağa kalkınca kapat
        interactionCanvasManager.ForceOpenCloseInteractionCanvas(false);
    }

    private void TimerComplete()
    {
        //Destroy Customer.
        //Destroy(gameObject);
        charModel.TriggerWin(false);
    }
    public void PickUpEnd()
    {
        PickUp.instance.OnCarryEnd?.Invoke();
    }

    private void Start()
    {
        if (movement.TrySetTarget(ChairManager.instance.GetAvailableChair()))
        {
            movement.OnReachedTarget += OrderFood;
        }
        interactionCanvasManager.gameObject.SetActive(false);
    }

    public void OrderFood()
    {
        interactionCanvasManager.gameObject.SetActive(true);
        orderConfig = FoodQuestManager.instance.RequestFood();
        timer.StartTimer(customerStayTimeInSecond);
        interactionCanvasManager.SetIcon(orderConfig.FoodSprite);
        movement.OnReachedTarget -= OrderFood;
    }

    private void Update()
    {
        timer.Tick(Time.deltaTime);
    }

    public void TakeFood()
    {
        Plate p = PickUp.instance.plate;
        int ingridientCount = 0;
        foreach (var item in p.ingridientItems)
        {
            foreach (var item2 in orderConfig.foodReciept)
            {
                if (item.foodIngridientConfig.ID == item2.ID)
                {
                    ingridientCount++;
                    break;
                }
            }
        }
        bool isSucces;
        if (ingridientCount != orderConfig.foodReciept.Count)
        {
            Debug.Log("Lose");
            isSucces = false;
        }
        else
        {
            Debug.Log("Win");
            isSucces = true;

            Money.SpawnMoneys(remainingTime, transform);

        }
        charModel.TriggerWin(isSucces);
        timer.ResetTimer();
        FoodQuestManager.instance.OnFoodDeliver?.Invoke(orderConfig, isSucces);

        Destroy(p.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionCanvasManager.Button.interactable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionCanvasManager.Button.interactable = false;
        }
    }
}
