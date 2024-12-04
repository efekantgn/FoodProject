using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public NPCCustomer customerPrefab;
    public Transform SpawnTransform;


    public void SpawnNPCs(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Invoke(nameof(SpawnNPC), 12 * i);
        }
    }

    [ContextMenu(nameof(SpawnNPC))]
    private void SpawnNPC()
    {
        Instantiate(customerPrefab, SpawnTransform);
    }
}