using UnityEngine;
using UnityEngine.UI;

public class InteractionCanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject InteractionPanel;
    [SerializeField] private bool isReadyToOpen = true;
    public Button button;

    public bool IsReadyToOpen { get => isReadyToOpen; set => isReadyToOpen = value; }

    public void CanvasSetActive(bool b)
    {
        if (!IsReadyToOpen) return;
        InteractionPanel.gameObject.SetActive(b);
    }
    public void ForceOpenCloseInteractionCanvas(bool b)
    {
        InteractionPanel.gameObject.SetActive(b);
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.collider.CompareTag("Player"))
    //         CanvasSetActive(true);
    // }
    // private void OnCollisionExit(Collision other)
    // {
    //     if (other.collider.CompareTag("Player"))
    //         CanvasSetActive(false);
    // }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            CanvasSetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            CanvasSetActive(false);
    }

}