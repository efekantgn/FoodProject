using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] private FoodSO foodConfig;
    public FoodSO FoodConfig { get => foodConfig; set => foodConfig = value; }

    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        Instantiate(FoodConfig.FoodPrefab, spawnPoint);
    }

    private bool isHeld = false;

    public void OnPickUp(GameObject player)
    {
        if (!isHeld)
        {
            isHeld = true;
            transform.SetParent(player.transform); // Player'in çocuğu yap
            transform.position = player.GetComponentInChildren<PlayerInteractor>().transform.position;
            transform.localScale *= .5f;
            Debug.Log("Tabak alındı.");
        }
    }

    public void OnDrop(GameObject player)
    {
        if (isHeld)
        {
            player.GetComponent<PlayerInteractor>().heldPlate = null;
            isHeld = false;
            transform.localScale *= 2f;
            transform.SetParent(null); // Tabak'ı serbest bırak
            Debug.Log("Tabak bırakıldı.");
            Destroy(gameObject);
        }
    }
}
