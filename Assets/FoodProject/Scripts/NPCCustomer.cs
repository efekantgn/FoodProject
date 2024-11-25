using UnityEngine;
using UnityEngine.AI;

public class NPCCustomer : MonoBehaviour
{
    private FoodSO orderConfig;
    private Transform targetChair;
    private NavMeshAgent agent;
    public Transform TransformContainer { get => transform; }

    public void MoveToChair()
    {
        agent.SetDestination(targetChair.position);
    }


}
