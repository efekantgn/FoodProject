using UnityEngine;

public class TableSpawner : MonoBehaviour
{
    public GameObject TableChairPrefab;
    public Transform TablesParent;
    private int rows = int.MaxValue;
    private int columns = 2;
    public float XSpacing;
    public float ZSpacing;


    public void SpawnTables(int count)
    {

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Hücrenin pozisyonunu hesapla
                Vector3 position = new Vector3(
                    TablesParent.position.x + col * XSpacing,
                    TablesParent.transform.position.y,
                    TablesParent.position.z + row * ZSpacing // Y ekseni aşağı doğru iniyor
                );

                // Prefab'ı instantiate et
                Instantiate(TableChairPrefab, position, Quaternion.identity, transform);
                count--;
                if (count == 0) return;
            }
        }
    }
}