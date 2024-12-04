using System;
using UnityEngine;

public class Chair : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    public Transform TargetTransform { get => targetTransform; set => targetTransform = value; }
    public bool IsOccupied { get; private set; }
    public Action OnChairOccupy;
    public Action OnChairVacate;

    private void Start()
    {
        ChairManager.instance.chairs.Add(this);
    }

    public void Occupy()
    {
        IsOccupied = true;
        OnChairOccupy?.Invoke();
    }

    public void Vacate()
    {
        IsOccupied = false;
        OnChairVacate?.Invoke();
    }


}

