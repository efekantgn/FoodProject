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
        // Offscreen pozisyonunu hesapla: Panelin genişliğini ekleyerek tamamen sahne dışına taşır
        offScreenPosition = new Vector2(Screen.width + panel.rect.width, panel.anchoredPosition.y);

        // Paneli başlangıçta sahne dışına yerleştir
        panel.anchoredPosition = offScreenPosition;
    }

    public void MoveOffScreen()
    {
        panel.DOAnchorPos(offScreenPosition, transitionDuration).SetEase(ease);
    }

    public void MoveOnScreen()
    {
        panel.DOAnchorPos(onScreenPosition, transitionDuration).SetEase(ease);
    }
}
