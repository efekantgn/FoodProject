using System;
using EfeTimer;
using UnityEngine;

public class NPCCustomer : MonoBehaviour
{
    private FoodSO orderConfig;
    private InteractionCanvasManager interactionCanvasManager;
    private NPCMovement movement;
    public Timer timer;
    [SerializeField] private float customerStayTime;
    private CharModelSelector charModel;
    private bool isYelled = false;


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
    }


    private void TimerUpdate(float value)
    {
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
        timer.StartTimer(customerStayTime);
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
                if (item.foodIngridient.ID == item2.ID)
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

        }
        charModel.TriggerWin(isSucces);
        FoodQuestManager.instance.OnFoodDeliver?.Invoke(orderConfig, isSucces);

        Destroy(p.gameObject);
        // Destroy(gameObject);
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
