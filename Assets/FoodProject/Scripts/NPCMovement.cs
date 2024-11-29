using UnityEngine;
using UnityEngine.AI;
using System;

public class NPCMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Action OnStartMoving;
    public Action OnReachedTarget;
    private Chair targetChair;

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

    private void Update()
    {
        if (targetChair != null && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                OnReachedTarget?.Invoke();
                targetChair = null;
            }
        }
    }
    private void OnEnable()
    {
        OnStartMoving += MovementStart;
        OnReachedTarget += TargetReached;
    }
    private void OnDisable()
    {
        OnStartMoving -= MovementStart;
        OnReachedTarget -= TargetReached;
    }
    public void MovementStart()
    {
        Debug.Log("MovementStart");
    }
    public void TargetReached()
    {
        Debug.Log("TargetReached");
    }
}
