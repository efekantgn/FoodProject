using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public NPCCustomer customerPrefab;
    public Transform SpawnTransform;

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            Invoke(nameof(SpawnNPC), 12 * i);
        }
    }

    [ContextMenu(nameof(SpawnNPC))]
    public void SpawnNPC()
    {
        Instantiate(customerPrefab, SpawnTransform);
    }
}