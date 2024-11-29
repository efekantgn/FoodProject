using EfeTimer;
using UnityEngine;

public class NPCCustomer : MonoBehaviour
{
    private FoodSO orderConfig;
    private InteractionCanvasManager interactionCanvasManager;
    private NPCMovement movement;
    private ProgressCanvas progressCanvas;
    private Timer timer;
    [SerializeField] private float customerStayTime;
    private CharModelSelector charModel;


    private void Awake()
    {
        interactionCanvasManager = GetComponentInChildren<InteractionCanvasManager>();
        progressCanvas = GetComponentInChildren<ProgressCanvas>();
        movement = GetComponentInChildren<NPCMovement>();
        charModel = GetComponentInChildren<CharModelSelector>();

        timer = new Timer();

        progressCanvas.GeneralPanel.SetActive(false);
        progressCanvas.timer = timer;

    }
    private void OnEnable()
    {
        interactionCanvasManager.Button.onClick.AddListener(TakeFood);
        interactionCanvasManager.Button.onClick.AddListener(PickUpEnd);

        //Timer Actions
        timer.OnTimerComplete += TimerComplete;
        timer.OnTimerUpdate += progressCanvas.UpdateProgressBar;
    }

    private void OnDisable()
    {
        interactionCanvasManager.Button.onClick.RemoveListener(TakeFood);
        interactionCanvasManager.Button.onClick.RemoveListener(PickUpEnd);

        //Timer Actions
        timer.OnTimerComplete -= TimerComplete;
        timer.OnTimerUpdate -= progressCanvas.UpdateProgressBar;
    }
    private void TimerComplete()
    {
        //Destroy Customer.
        //Destroy(gameObject);
        charModel.OnPlayerStandUp?.Invoke();
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
    }

    public void OrderFood()
    {
        orderConfig = FoodQuestManager.instance.RequestFood();
        timer.StartTimer(customerStayTime);
        progressCanvas.GeneralPanel.SetActive(true);
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

