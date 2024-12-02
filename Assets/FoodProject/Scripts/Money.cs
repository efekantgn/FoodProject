using UnityEngine;

public class Money : MonoBehaviour
{
    //public int CurrentMoney;
    public MoneyEntity DollarPrefab;
    public MoneyEntity CoinPrefab;


    public void SpawnMoneys(float value, Transform spawnTransform)
    {
        int CurrentMoney = (int)value;
        Debug.Log(CurrentMoney);
        int CoinCount = CurrentMoney % 10;
        int DollarCount = (CurrentMoney - CoinCount) / 10;

        for (int i = 0; i < DollarCount; i++)
        {
            Instantiate(DollarPrefab).transform.position = spawnTransform.position;
        }
        for (int i = 0; i < CoinCount; i++)
        {
            Instantiate(CoinPrefab).transform.position = spawnTransform.position;
        }
    }
}