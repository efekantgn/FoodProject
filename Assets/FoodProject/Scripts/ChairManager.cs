using System.Collections.Generic;
using UnityEngine;

public class ChairManager : MonoBehaviour
{
    public Chair[] chairs;

    public static ChairManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// Boş bir sandalye bul ve döndür.
    /// </summary>
    public Chair GetAvailableChair()
    {
        // Boş sandalyeleri bir listeye al
        List<Chair> availableChairs = new List<Chair>();
        foreach (var chair in chairs)
        {
            if (!chair.IsOccupied)
            {
                availableChairs.Add(chair);
            }
        }

        // Eğer hiç boş sandalye yoksa null döndür
        if (availableChairs.Count == 0)
        {
            Debug.Log("No available chairs found.");
            return null;
        }

        // Rastgele bir boş sandalye seç
        return availableChairs[Random.Range(0, availableChairs.Count)]; ;
    }
}
