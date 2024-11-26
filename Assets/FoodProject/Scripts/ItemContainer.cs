using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    [SerializeField] private Canvas InteractionCanvas;


    public void CanvasOpenClose(bool b)
    {
        InteractionCanvas.gameObject.SetActive(b);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
            CanvasOpenClose(true);
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player"))
            CanvasOpenClose(false);
    }

}