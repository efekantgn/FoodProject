using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class CharModelSelector : MonoBehaviour
{
    private readonly string isWalkingBool = "isWalking";
    private readonly string isSittingBool = "isSitting";
    private readonly string YellTrigger = "Yell";
    private readonly string LoseTrigger = "Lose";
    private readonly string WinTrigger = "Win";
    private readonly string StandUpTrigger = "StandUp";
    private readonly string SitDownTrigger = "SitDown";

    [SerializeField] private GameObject[] models;
    public GameObject selectedChar;
    private Animator animator;

    public NPCMovement movement;

    public Action OnPlayerStandUp;

    private void Awake()
    {
        movement = GetComponentInParent<NPCMovement>();
        EnableRandomModel();
    }
    private void OnEnable()
    {
        movement.OnStartMoving += MoveStart;
        movement.OnReachedTarget += ReachedTarget;
        OnPlayerStandUp += PlayerStandUp;
    }

    private void PlayerStandUp()
    {
        animator.SetBool(isWalkingBool, true);
        movement.Exit();
    }

    private void ReachedTarget()
    {
        animator.SetTrigger(SitDownTrigger);
        animator.SetBool(isWalkingBool, false);
    }

    private void MoveStart()
    {
        animator.SetBool(isWalkingBool, true);
    }

    private void OnDisable()
    {
        movement.OnStartMoving -= MoveStart;
        movement.OnReachedTarget -= ReachedTarget;
        OnPlayerStandUp -= PlayerStandUp;

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
