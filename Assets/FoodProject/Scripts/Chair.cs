using System;
using UnityEngine;

public class Chair : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    public Transform TargetTransform { get => targetTransform; set => targetTransform = value; }
    public bool IsOccupied { get; private set; }
    public Action OnChairOccupy;
    public Action OnChairVacate;

    private void OnEnable()
    {
        OnChairOccupy += TestOccupy;
        OnChairVacate += TestVacate;
    }

    private void TestVacate()
    {
        Debug.Log("TestVacate");
    }

    private void OnDisable()
    {
        OnChairOccupy -= TestOccupy;
        OnChairVacate -= TestVacate;
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

    public void TestOccupy()
    {
        Debug.Log("TestOccupy");
    }
}

