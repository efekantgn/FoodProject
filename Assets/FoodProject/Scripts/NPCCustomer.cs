using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class NPCCustomer : MonoBehaviour
{
    private FoodSO orderConfig;

    private InteractionCanvasManager interactionCanvasManager;

    private void Awake()
    {
        interactionCanvasManager = GetComponentInChildren<InteractionCanvasManager>();
    }
    private void OnEnable()
    {
        interactionCanvasManager.button.onClick.AddListener(TakeFood);
    }
    private void OnDisable()
    {
        interactionCanvasManager.button.onClick.RemoveListener(TakeFood);

    }
    private void Start()
    {
        interactionCanvasManager.ForceOpenCloseInteractionCanvas(false);
        OrderFood();
    }

    public void OrderFood()
    {
        orderConfig = FoodQuestManager.instance.RequestFood();
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
        FoodQuestManager.instance.OnFoodDeliver?.Invoke(orderConfig, isSucces);
        Destroy(p.gameObject);
        Destroy(gameObject);
    }
}
