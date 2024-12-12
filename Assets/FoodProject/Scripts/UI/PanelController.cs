using UnityEngine;
using DG.Tweening;

public class PanelController : MonoBehaviour
{
    public RectTransform panel; // Panelin RectTransform referansı
    public Vector2 onScreenPosition = Vector2.zero; // Panelin sahnedeki pozisyonu
    public float transitionDuration = 0.5f; // Geçiş süresi
    public Ease ease;
    private Vector2 offScreenPosition; // Panelin sahne dışındaki pozisyonu

    private void Start()
    {
        // Offscreen pozisyonunu ekranın genişliğine göre ayarla
        offScreenPosition = new Vector2(Screen.width, panel.anchoredPosition.y);

        // Paneli başlangıçta sahne dışına yerleştir
        panel.anchoredPosition = offScreenPosition;

    }

    [ContextMenu("TogglePanel")]
    public void TogglePanel(bool isOpen)
    {
        if (!isOpen)
        {
            // Paneli sahne dışına kaydır (deaktif et)
            panel.DOAnchorPos(offScreenPosition, transitionDuration).SetEase(ease);
        }
        else
        {
            // Paneli sahne içine kaydır (aktif et)
            panel.DOAnchorPos(onScreenPosition, transitionDuration).SetEase(ease);
        }
    }
}
