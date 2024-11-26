using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    private Plate plate;
    private Button button;
    public Transform PickUpTransform;

    private void Awake()
    {
        plate = GetComponentInParent<Plate>();
        button = GetComponentInParent<Button>();
    }
    private void OnEnable()
    {
        button.onClick.AddListener(PickUpGameObject);
    }
    private void OnDisable()
    {

        button.onClick.RemoveListener(PickUpGameObject);
    }
    public void PickUpGameObject()
    {
        plate.transform.parent = PickUpTransform;
        plate.transform.position = PickUpTransform.position;
    }
}