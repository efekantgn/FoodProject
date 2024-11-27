using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public NPCCustomer customerPrefab;
    public Transform SpawnTransform;

    [ContextMenu(nameof(SpawnNPC))]
    public void SpawnNPC()
    {
        Instantiate(customerPrefab, SpawnTransform);
    }
}