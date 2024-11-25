using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovementManager : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    private Vector2 moveInput;
    private PlayerInputManager inputManager;
    private bool isMoving = false;

    public bool IsMoving
    {
        get => isMoving; set
        {
            if (isMoving == value) return;

            isMoving = value;
        }
    }

    private void Awake()
    {
        inputManager = GetComponentInParent<PlayerInputManager>();
        rb = GetComponentInChildren<Rigidbody>();
    }

    private void OnEnable()
    {
        inputManager.OnMovePerform.AddListener(MovePerform);
        inputManager.OnMoveEnd.AddListener(MoveEnd);
    }

    private void OnDisable()
    {
        inputManager.OnMovePerform.RemoveListener(MovePerform);
        inputManager.OnMoveEnd.RemoveListener(MoveEnd);
    }

    private void MovePerform(Vector2 value)
    {
        //Debug.Log($"Move Perform at: {value}");
        moveInput = value;
        IsMoving = true;
    }

    private void MoveEnd(Vector2 value)
    {
        //Debug.Log($"Move Ended at: {value}");
        moveInput = value;
        IsMoving = false;
    }

    private void FixedUpdate()
    {
        // Hareketi Rigidbody'e uygula
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
        rb.velocity = movement * speed + new Vector3(0, rb.velocity.y, 0); // Y eksenini sabitle
    }
}

