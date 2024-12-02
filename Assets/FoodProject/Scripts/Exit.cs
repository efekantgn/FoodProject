using UnityEngine;

public class Exit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            Destroy(other.GetComponentInParent<NPCCustomer>().gameObject);
        }
    }
}