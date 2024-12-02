using System;
using UnityEngine;
using UnityEngine.UI;

public class NPCEmotionSystem : MonoBehaviour
{
    public Sprite Happy;
    public Sprite Normal;
    public Sprite Sad;
    public Sprite Angry;
    public Image NPCState;

    public NPCCustomer customer;

    private void Awake()
    {
        customer = GetComponentInParent<NPCCustomer>();
    }
    private void OnEnable()
    {
        customer.timer.OnTimerUpdate += OnTimeChanged;
    }

    private void OnTimeChanged(float value)
    {
        if (value < customer.timer.Duration && value > customer.timer.Duration / 3 * 2)
        {
            NPCState.sprite = Happy;
        }
        else if (value < customer.timer.Duration / 3 * 2 && value > customer.timer.Duration / 3)
        {
            NPCState.sprite = Normal;
        }
        else if (value < customer.timer.Duration / 3 && 0 < value)
        {
            NPCState.sprite = Sad;
        }
        else if (value == 0)
        {
            NPCState.sprite = Angry;
        }
    }

    private void OnDisable()
    {
        customer.timer.OnTimerUpdate -= OnTimeChanged;
    }
    private void LateUpdate()
    {
        NPCState.transform.forward = -Camera.main.transform.forward;
    }
}