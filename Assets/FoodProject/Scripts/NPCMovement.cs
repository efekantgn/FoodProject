using UnityEngine;
using UnityEngine.AI;
using System;

public class NPCMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    public Action OnStartMoving;
    public Action OnReachedTarget;
    public Chair targetChair;
    public Action OnPlayerStandUp;
    private bool hasReachedToTarget = false;

    private void Awake()
    {
        agent = GetComponentInChildren<NavMeshAgent>();
    }

    private void OnEnable()
    {
        OnPlayerStandUp += CustomerLeavesChair;
        OnReachedTarget += SnapToTarget;
    }

    private void CustomerLeavesChair()
    {
        agent.enabled = true;

        targetChair.Vacate();
        targetChair = null;
    }

    private void OnDisable()
    {
        OnPlayerStandUp -= CustomerLeavesChair;
        OnReachedTarget -= SnapToTarget;
    }

    public void SnapToTarget()
    {
        if (targetChair == null) return;

        agent.enabled = false;
        transform.position = targetChair.TargetTransform.position;
        transform.rotation = targetChair.TargetTransform.rotation;

    }

    /// <summary>
    /// NPC'yi hedef sandalyeye yönlendir.
    /// </summary>
    /// <param name="chair">Gidilecek sandalye</param>
    public bool TrySetTarget(Chair chair)
    {
        if (chair == null || chair.IsOccupied)
        {
            Debug.LogError("Geçersiz veya dolu bir sandalye hedef olarak atanamaz.");
            return false;
        }

        targetChair = chair;

        // Sandalyeyi dolu işaretle
        chair.Occupy();

        // Hareketi başlat
        agent.SetDestination(chair.TargetTransform.position);
        OnStartMoving?.Invoke();
        return true;
    }

    public void Exit()
    {
        agent.SetDestination(GameObject.FindWithTag("Exit").transform.position);
        hasReachedToTarget = false;
        OnStartMoving?.Invoke();
    }

    private void Update()
    {
        if (!hasReachedToTarget && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                OnReachedTarget?.Invoke();
                hasReachedToTarget = true;
            }
        }
    }
}
