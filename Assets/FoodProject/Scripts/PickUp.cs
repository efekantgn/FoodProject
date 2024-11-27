using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public Transform PickUpTransform;
    public Plate plate;

    public void PickUpGameObject()
    {
        plate.transform.parent = PickUpTransform;
        plate.transform.position = PickUpTransform.position;
    }
}