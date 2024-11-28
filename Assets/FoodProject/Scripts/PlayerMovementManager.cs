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
    public float MovementSpeed = 5f;
    public float Rotationspeed = 5f;
    private Rigidbody rb;
    private Vector2 moveInput;
    private PlayerInputManager inputManager;
    private bool isMoving = false;
    Vector3 moveDirection = Vector3.zero;
    Transform cameraTransform;

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
        cameraTransform = Camera.main.transform;
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
    }

    private void FixedUpdate()
    {
        // Hareketi Rigidbody'e uygula
        if (!isMoving) return;
        moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        if (cameraTransform != null)
        {
            // Kameraya göre joystick girdisini döndür
            Vector3 cameraForward = cameraTransform.forward;
            Vector3 cameraRight = cameraTransform.right;

            // Y düzlemi üzerinde hareket yönü
            cameraForward.y = 0;
            cameraRight.y = 0;

            moveDirection = (cameraForward.normalized * moveInput.y) + (cameraRight.normalized * moveInput.x);
        }
        rb.velocity = moveDirection.normalized * MovementSpeed;

        if (rb.velocity == Vector3.zero) IsMoving = false;
        // Karakteri hareket ettiği yöne döndür
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * Rotationspeed));
    }
}

