using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class CharModelSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] models;


    private void Start()
    {
        EnableRandomModel();
    }

    public void EnableRandomModel()
    {
        foreach (var model in models)
        {
            model.gameObject.SetActive(false);
        }

        int rnd = Random.Range(0, models.Length);
        models[rnd].gameObject.SetActive(true);
    }
}