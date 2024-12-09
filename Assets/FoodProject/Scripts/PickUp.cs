using System;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public Transform PickUpTransform;
    public Plate plate;
    public Action OnCarryStart;
    public Action OnCarryEnd;
    public static PickUp instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void PickUpGameObject(Plate p)
    {
        plate = p;
        plate.transform.parent = PickUpTransform;
        plate.transform.position = PickUpTransform.position;
        plate.transform.rotation = PickUpTransform.rotation;
        PlateSpawner.instance.plates.Remove(plate);
        OnCarryStart?.Invoke();
    }
    public void RemovePlate()
    {
        Destroy(plate.gameObject);
        plate = null;
        OnCarryEnd?.Invoke();
    }
}