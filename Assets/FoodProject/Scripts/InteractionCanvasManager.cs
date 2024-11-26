using UnityEngine;

public class InteractionCanvasManager : MonoBehaviour
{
    [SerializeField] private Canvas InteractionCanvas;
    [SerializeField] private bool isReadyToOpen = true;

    public bool IsReadyToOpen { get => isReadyToOpen; set => isReadyToOpen = value; }

    public void CanvasSetActive(bool b)
    {
        if (!IsReadyToOpen) return;
        InteractionCanvas.gameObject.SetActive(b);
    }
    public void ForceOpenCloseInteractionCanvas(bool b)
    {
        InteractionCanvas.gameObject.SetActive(b);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
            CanvasSetActive(true);
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player"))
            CanvasSetActive(false);
    }

}