using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;

public class MoneyEntity : MonoBehaviour
{
    public int Amount;
    [Header("Animation Settings")]
    public float spreadRadius = 1.5f; // Objelerin etrafa saçılacağı yarıçap
    public float animationDuration = 0.5f; // Animasyonun süresi
    public float upwardDistance = 2f; // Yukarı doğru çıkma mesafesi
    private Vector3 initialPosition;
    private Action OnAnimatonComplete;
    private SphereCollider col;

    private void Awake()
    {
        col = GetComponentInChildren<SphereCollider>();
    }
    private void Start()
    {
        Animation();
    }
    private void OnEnable()
    {
        OnAnimatonComplete += () => col.enabled = true;
    }
    private void Animation()
    {
        // Objeyi spawn olduğunda animasyon başlat
        initialPosition = transform.position;

        // Rastgele bir hedef pozisyon hesapla
        Vector3 randomOffset = new Vector3(
            Random.Range(-spreadRadius, spreadRadius),
            0,
            Random.Range(-spreadRadius, spreadRadius)
        );

        Vector3 targetPosition = initialPosition + randomOffset;

        // Yukarı doğru hareket ve geri dönüş animasyonu
        transform.DOMoveY(initialPosition.y + upwardDistance, animationDuration / 2)
            .SetEase(Ease.OutQuad) // Yukarı çıkarken yavaşlayarak hareket et
            .OnComplete(() =>
            {
                transform.DOMoveY(initialPosition.y, animationDuration / 2)
                    .SetEase(Ease.InQuad)
                    .OnComplete(() =>
                    {
                        // X-Z eksenindeki saçılma hareketi
                        transform.DOMoveX(targetPosition.x, animationDuration).SetEase(Ease.OutCirc);
                        transform.DOMoveZ(targetPosition.z, animationDuration).SetEase(Ease.OutCirc);

                        // Animasyon tamamlandığında onComplete Action'ını tetikle
                        OnAnimatonComplete?.Invoke();
                    });
            });
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCurrency pc = other.transform.parent.GetComponentInChildren<PlayerCurrency>();
            if (pc != null)
            {
                pc.MoneyAmount += Amount;
            }
            Destroy(gameObject);
        }
    }
}
