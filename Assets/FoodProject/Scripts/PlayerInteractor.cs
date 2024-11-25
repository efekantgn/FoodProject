using System;
using System.Collections.Generic;
using EfeTimer;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteractor : MonoBehaviour
{
    public float sphereRadius = 1f;
    public float maxDistance = 10f;
    public LayerMask interactableLayer;
    //ToDO TimerConfig scriptable Object
    private InteractionTimer timer;
    public UnityEvent<float> OnTimerUpdate;
    public Transform TransformContainer => transform;
    private Plate currentPlate;
    public Plate heldPlate;
    private Plate tempItem;


    private void Awake()
    {
        timer = GetComponent<InteractionTimer>();
    }
    private void OnEnable()
    {
        timer.OnTimerReset.AddListener(TimerReset);
        timer.OnTimerUpdate.AddListener(TimerUpdate);
        timer.OnTimerComplete.AddListener(TimerComplete);
    }
    private void OnDisable()
    {
        timer.OnTimerReset.RemoveListener(TimerReset);
        timer.OnTimerUpdate.RemoveListener(TimerUpdate);
        timer.OnTimerComplete.RemoveListener(TimerComplete);
    }

    public void TimerUpdate(float value)
    {
        OnTimerUpdate?.Invoke(value / timer.Duration);
    }

    public void TimerComplete()
    {
        if (heldPlate != null)
        {
            heldPlate.OnDrop(gameObject);
            heldPlate = null;
        }
        else if (tempItem != null)
        {
            heldPlate = tempItem;
            tempItem = null;
            heldPlate.OnPickUp(gameObject);
        }
    }
    public void TimerReset()
    {
        TimerUpdate(0);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Plate interactable))
        {
            tempItem = interactable;
            timer.StartTimer();
        }
        else if (other.TryGetComponent(out NPCCustomer customer))
        {
            timer.StartTimer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(tempItem == null);
        if (tempItem != null)
        {
            timer.ResetTimer();
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}