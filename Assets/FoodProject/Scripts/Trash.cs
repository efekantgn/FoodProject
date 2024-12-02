using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] private InteractionCanvasManager interactionCanvasManager;

    private void OnEnable()
    {
        interactionCanvasManager.Button.onClick.AddListener(RemovePlate);
    }
    private void OnDisable()
    {
        interactionCanvasManager.Button.onClick.RemoveListener(RemovePlate);
    }
    public void RemovePlate()
    {
        PickUp.instance.RemovePlate();
    }
}