using UnityEngine;

public class MoneyEntity : MonoBehaviour
{
    public int Amount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            PlayerCurrency pc = other.transform.parent.GetComponentInChildren<PlayerCurrency>();
            if (pc != null)
            {
                pc.MoneyAmount += Amount;
            }
            Destroy(gameObject);
        }
    }
}