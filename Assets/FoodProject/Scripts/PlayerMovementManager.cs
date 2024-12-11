using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
public enum MovementType
{
    Joystick,
    ClickToMove
}
public class PlayerMovementManager : MonoBehaviour
{
    private Animator animator;
    private readonly string Animator_IsWalking = "IsWalking";
    private readonly string Animator_IsCarry = "IsCarry";
    public float MovementSpeed = 5f;
    public float Rotationspeed = 5f;
    private Rigidbody rb;
    private Vector2 moveInput;
    [SerializeField] private MovementType movementType;
    private PlayerInputManager inputManager;
    private NavMeshAgent agent;
    [SerializeField] private LayerMask PlayerMoveLayer;
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

    public MovementType MovementType
    {
        get => movementType; set => movementType = value;
    }

    private void Awake()
    {
        inputManager = GetComponentInParent<PlayerInputManager>();
        rb = GetComponentInChildren<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        agent = GetComponentInChildren<NavMeshAgent>();
        cameraTransform = Camera.main.transform;
    }
    private void Start()
    {
        animatorStartSpeed = animator.speed;
        agent.speed = MovementSpeed;
    }

    private void OnEnable()
    {
        inputManager.OnMovePerform.AddListener(MovePerform);
        inputManager.OnMoveEnd.AddListener(MoveEnd);
        inputManager.OnTouch += MoveToPoint;

        OnPlayerMove += PlayerMoveStart;
        OnPlayerStop += PlayerMoveStop;
        PickUp.instance.OnCarryStart += PickUpStart;
        PickUp.instance.OnCarryEnd += PickUpEnd;
    }
    private void OnDisable()
    {
        inputManager.OnMovePerform.RemoveListener(MovePerform);
        inputManager.OnMoveEnd.RemoveListener(MoveEnd);
        inputManager.OnTouch -= MoveToPoint;

        OnPlayerMove -= PlayerMoveStart;
        OnPlayerStop -= PlayerMoveStop;
    }

    private void MoveToPoint(Vector2 vector)
    {
        if (movementType != MovementType.ClickToMove) return;

        Ray ray = Camera.main.ScreenPointToRay(vector);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (!IsInLayerMask(PlayerMoveLayer, hitInfo.collider.gameObject)) return;

            OnPlayerMove?.Invoke();
            agent.SetDestination(hitInfo.point);
        }
    }

    private bool IsInLayerMask(LayerMask mask, GameObject go)
    {
        return (mask.value & (1 << go.layer)) != 0;
    }

    private void PickUpEnd()
    {
        animator.SetBool(Animator_IsCarry, false);
    }

    private void PickUpStart()
    {
        animator.SetBool(Animator_IsCarry, true);
    }


    private void MovePerform(Vector2 value)
    {
        if (movementType != MovementType.Joystick) return;
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
        switch (MovementType)
        {
            case MovementType.Joystick:
                JoystickMovement();
                break;
            case MovementType.ClickToMove:
                ReachedToTarget();
                break;
        }
    }

    private void JoystickMovement()
    {
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

    private void ReachedToTarget()
    {
        if (!agent.hasPath) return;
        if (Vector3.Distance(agent.destination, transform.position) <= agent.stoppingDistance)
        {
            OnPlayerStop?.Invoke();
        }
    }
}
