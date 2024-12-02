using UnityEngine;
using UnityEngine.UI;

public class InteractionCanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject InteractionPanel;
    [SerializeField] private bool isReadyToOpen = true;
    public Button Button;
    public Image Image;

    public bool IsReadyToOpen { get => isReadyToOpen; set => isReadyToOpen = value; }

    public void CanvasSetActive(bool b)
    {
        if (!IsReadyToOpen) return;
        InteractionPanel.SetActive(b);
    }
    public void ForceOpenCloseInteractionCanvas(bool b)
    {
        InteractionPanel.SetActive(b);
    }

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
    public void SetIcon(Sprite sprite)
    {
        Image.sprite = sprite;
    }
    private void LateUpdate()
    {
        //InteractionPanel.transform.forward = -Camera.main.transform.forward;
        InteractionPanel.transform.LookAt(Camera.main.transform.position * -1);
        InteractionPanel.transform.right = Camera.main.transform.right;

    }
}