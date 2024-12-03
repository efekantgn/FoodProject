using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class CharModelSelector : MonoBehaviour
{
    private readonly string isWalkingBool = "isWalking";
    private readonly string YellTrigger = "Yell";
    private readonly string LoseTrigger = "Lose";
    private readonly string WinTrigger = "Win";
    private readonly string SitDownTrigger = "SitDown";

    [SerializeField] private GameObject[] models;
    public GameObject selectedChar;
    private Animator animator;

    public NPCMovement movement;


    private void Awake()
    {
        movement = GetComponentInParent<NPCMovement>();
        EnableRandomModel();
    }
    private void OnEnable()
    {
        movement.OnStartMoving += MoveStart;
        movement.OnReachedTarget += ReachedTarget;
        movement.OnPlayerStandUp += PlayerStandUp;
    }

    private void PlayerStandUp()
    {
        animator.SetBool(isWalkingBool, true);
        movement.Exit();
    }

    private void ReachedTarget()
    {
        animator.SetTrigger(SitDownTrigger);
        //TODO: Boş Sandalye yoksa null hatası veriyor.
        movement.transform.LookAt(movement.targetChair.transform.parent, Vector3.up);
        animator.SetBool(isWalkingBool, false);
    }

    private void MoveStart()
    {
        animator.SetBool(isWalkingBool, true);
    }
    public void YellTimer()
    {
        animator.SetTrigger(YellTrigger);
    }

    private void OnDisable()
    {
        movement.OnStartMoving -= MoveStart;
        movement.OnReachedTarget -= ReachedTarget;
        movement.OnPlayerStandUp -= PlayerStandUp;
    }

    public void EnableRandomModel()
    {
        foreach (var model in models)
        {
            model.gameObject.SetActive(false);
        }

        int rnd = UnityEngine.Random.Range(0, models.Length);
        selectedChar = models[rnd].gameObject;
        selectedChar.SetActive(true);
        animator = selectedChar.GetComponentInParent<Animator>();
    }

    public void TriggerWin(bool isSucces)
    {
        switch (isSucces)
        {
            case true:
                animator.SetTrigger(WinTrigger);
                break;
            case false:
                animator.SetTrigger(LoseTrigger);
                break;
        }
    }
}
