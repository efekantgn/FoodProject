using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public NPCCustomer customerPrefab;
    public Transform SpawnTransform;
    public Exit Exit;
    private int RemainingNPCCount = 0;
    public Action OnLastNPCLeft;

    private void OnEnable()
    {
        Exit.OnNPCLeavesScene += CheckIsLevelEnd;
    }
    private void OnDisable()
    {
        Exit.OnNPCLeavesScene -= CheckIsLevelEnd;
    }

    private void CheckIsLevelEnd()
    {
        RemainingNPCCount--;
        if (RemainingNPCCount <= 0)
        {
            Warning.instance.GiveWarning("Level End");
            OnLastNPCLeft?.Invoke();
        }
    }

    public void SpawnNPCs(int count)
    {
        RemainingNPCCount = count;
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