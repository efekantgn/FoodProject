using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class MoneyCollectEffect : MonoBehaviour
{
    public MoneyUI moneyUI; // Hedef UI ikonu (RectTransform)
    public float duration = 1.0f; // Hareket süresi
    public Ease easeType = Ease.InOutQuad; // Hareket eğrisi
    private Camera mainCamera;
    public Action OnMoveComplete;

    private void Start()
    {
        // Ana kamerayı bul
        mainCamera = Camera.main;
    }
    private void Awake()
    {
        moneyUI = FindObjectOfType<MoneyUI>();
    }

    public void MoveToTarget()
    {
        Vector3 targetScreenPosition = moneyUI.Icon.rectTransform.position;
        // UI ikonu dünya uzayında bir pozisyona çevir
        Vector3 targetWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(targetScreenPosition.x, targetScreenPosition.y, mainCamera.nearClipPlane));
        targetWorldPosition.z = transform.position.z; // Z pozisyonunu sabit tut

        // GameObject'i UI ikonuna doğru taşı
        transform.DOMove(targetWorldPosition, duration)
                 .SetEase(easeType)
                 .OnComplete(() => OnMoveComplete?.Invoke());
    }
}
