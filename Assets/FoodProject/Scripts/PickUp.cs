using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public Transform PickUpTransform;
    public Plate plate;

    public static PickUp instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void PickUpGameObject()
    {
        plate.transform.parent = PickUpTransform;
        plate.transform.position = PickUpTransform.position;
        PlateSpawner.instance.plates.Remove(plate);

    }
}