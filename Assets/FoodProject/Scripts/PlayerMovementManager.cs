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
    private Animator animator;
    private readonly string Animator_IsWalking = "IsWalking";
    private readonly string Animator_IsCarry = "IsCarry";
    public float MovementSpeed = 5f;
    public float Rotationspeed = 5f;
    private Rigidbody rb;
    private Vector2 moveInput;
    private PlayerInputManager inputManager;
    private bool isMoving = false;
    Vector3 moveDirection = Vector3.zero;
    Transform cameraTransform;
    private float animatorStartSpeed;
    public event Action OnPlayerMove;
    public event Action OnPlayerStop;

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
        animator = GetComponentInChildren<Animator>();
        cameraTransform = Camera.main.transform;
    }
    private void Start()
    {
        animatorStartSpeed = animator.speed;
    }

    private void OnEnable()
    {
        inputManager.OnMovePerform.AddListener(MovePerform);
        inputManager.OnMoveEnd.AddListener(MoveEnd);
        OnPlayerMove += PlayerMoveStart;
        OnPlayerStop += PlayerMoveStop;
        PickUp.instance.OnCarryStart += PickUpStart;
        PickUp.instance.OnCarryEnd += PickUpEnd;
    }

    private void PickUpEnd()
    {
        animator.SetBool(Animator_IsCarry, false);
    }

    private void PickUpStart()
    {
        animator.SetBool(Animator_IsCarry, true);
    }

    private void OnDisable()
    {
        inputManager.OnMovePerform.RemoveListener(MovePerform);
        inputManager.OnMoveEnd.RemoveListener(MoveEnd);
        OnPlayerMove -= PlayerMoveStart;
        OnPlayerStop -= PlayerMoveStop;
    }

    private void MovePerform(Vector2 value)
    {
        //Debug.Log($"Move Perform at: {value}");
        moveInput = value;
        OnPlayerMove?.Invoke();
    }

    private void MoveEnd(Vector2 value)
    {
        //Debug.Log($"Move Ended at: {value}");
        moveInput = value;
    }
    private void PlayerMoveStart()
    {
        IsMoving = true;
        animator.SetBool(Animator_IsWalking, true);
        animator.speed = MovementSpeed;
    }
    private void PlayerMoveStop()
    {
        IsMoving = false;
        animator.SetBool(Animator_IsWalking, false);
        animator.speed = animatorStartSpeed;
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

        if (rb.velocity == Vector3.zero)
            OnPlayerStop?.Invoke();

        // Karakteri hareket ettiği yöne döndür
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * Rotationspeed));
    }
}
