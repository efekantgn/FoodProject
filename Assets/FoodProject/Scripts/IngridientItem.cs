using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
public enum FoodState
{
    Raw, Cooked, ReadyToUse, Burned
}
public class IngridientItem : MonoBehaviour
{
    public FoodIngridientSO foodIngridientConfig;
    public FoodUpgradeTierSO tierConfig;

    public GameObject Raw;
    public GameObject Cooked;
    public GameObject Burned;
    public FoodState foodState = FoodState.Raw;


    [Header("Transport")]
    public float moveDuration = 2f; // Hareket süresi
    public float scaleMultiplier = 1.5f; // Büyüme/küçülme oranı
    public float rotationSpeed = 360f; // Dönüş hızı

    public UnityEvent OnMoveStart; // Hareket başlangıcında tetiklenir
    public UnityEvent OnMoveComplete; // Hareket bitiminde tetiklenir
    public UnityEvent<GameObject> OnMoveCompleteCarry; // Hareket bitiminde tetiklenir


    public void StartMovement(Vector3 targetPosition)
    {
        // Hareket başlarken UnityEvent tetiklenir
        OnMoveStart?.Invoke();

        // Orijinal değerleri kaydet
        Vector3 originalScale = transform.localScale;

        // Animasyon sırasını oluştur
        Sequence movementSequence = DOTween.Sequence();

        // Eğlenceli hareket animasyonu
        movementSequence.Append(transform.DOMove(targetPosition, moveDuration).SetEase(Ease.InOutQuad));

        // Büyüme/küçülme efekti
        movementSequence.Join(transform.DOScale(originalScale * scaleMultiplier, moveDuration / 2).SetLoops(2, LoopType.Yoyo));

        // Dönme efekti
        movementSequence.Join(transform.DORotate(Vector3.up * rotationSpeed, moveDuration, RotateMode.FastBeyond360));

        // Hareket tamamlandığında UnityEvent tetiklenir
        movementSequence.OnComplete(() =>
        {
            OnMoveComplete?.Invoke();
            OnMoveCompleteCarry?.Invoke(gameObject);
        });

        // Hareketi başlat
        movementSequence.Play();
    }
    public void DestroyThisItem()
    {
        Destroy(gameObject);
    }

    public void ProcessItem()
    {
        Raw.SetActive(false);
        Cooked.SetActive(true);
        foodState = FoodState.Cooked;
    }
}