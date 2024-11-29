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
        foreach (var chair in chairs)
        {
            if (!chair.IsOccupied)
            {
                Debug.Log($"{chair.name} selected");
                return chair;
            }
        }
        Debug.Log($"null");

        return null; // Boş sandalye bulunamadı
    }
}
