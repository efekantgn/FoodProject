using System;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public Action OnNPCLeavesScene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            Destroy(other.GetComponentInParent<NPCCustomer>().gameObject);
            OnNPCLeavesScene?.Invoke();
        }
    }
}