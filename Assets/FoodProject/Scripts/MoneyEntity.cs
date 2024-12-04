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
    private MoneyCollectEffect collectEffect;
    private PlayerCurrency playerCurrency;

    private void Awake()
    {
        col = GetComponentInChildren<SphereCollider>();
        collectEffect = GetComponentInChildren<MoneyCollectEffect>();
        playerCurrency = FindObjectOfType<PlayerCurrency>();
    }
    private void Start()
    {
        Animation();
    }
    private void OnEnable()
    {
        OnAnimatonComplete += collectEffect.MoveToTarget;
        collectEffect.OnMoveComplete += IncreaseMoney;
    }
    private void OnDisable()
    {
        collectEffect.OnMoveComplete -= IncreaseMoney;
        OnAnimatonComplete -= collectEffect.MoveToTarget;
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
        transform.DOMoveY(initialPosition.y + upwardDistance, animationDuration / 3)
            .SetEase(Ease.OutBack) // Yukarı çıkarken mutluluk hissi veren esnek bir hareket
            .OnComplete(() =>
            {
                // X-Z eksenindeki saçılma hareketi
                transform.DOMove(targetPosition, animationDuration)
                    .SetEase(Ease.OutQuad) // Daha kavisli bir dağılım
                    .OnStart(() =>
                    {
                        // Objeye hafif bir döndürme animasyonu ekle
                        transform.DORotate(new Vector3(0, 360, 0), animationDuration, RotateMode.FastBeyond360)
                            .SetEase(Ease.Linear); // Sürekli dönen hareket
                    })
                    .OnComplete(() =>
                    {
                        // Animasyon tamamlandığında onComplete Action'ını tetikle
                        OnAnimatonComplete?.Invoke();
                    });
            });
    }


    private void IncreaseMoney()
    {
        if (playerCurrency != null)
        {
            playerCurrency.CurrentMoney += Amount;
        }
        Destroy(gameObject);
    }
}
