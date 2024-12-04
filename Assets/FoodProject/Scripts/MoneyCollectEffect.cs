using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEditor.Rendering;

public class MoneyCollectEffect : MonoBehaviour
{
    public Action OnMoveComplete;
    public float moveSpeed = 5f; // Objeyi hedefe taşırkenki hız
    private bool isMovingToCanvas = false;
    Transform Player;

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player").transform;
    }

    public void MoveToTarget()
    {
        isMovingToCanvas = true;
    }
    private void Update()
    {
        if (!isMovingToCanvas) return;

        transform.position = Vector3.MoveTowards(transform.position, Player.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, Player.position) < 0.1f)
        {
            OnMoveComplete?.Invoke();
            Destroy(gameObject);
        }

    }
}
