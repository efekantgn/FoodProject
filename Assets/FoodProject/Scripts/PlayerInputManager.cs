using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private PlayerInputActions inputActions;

    public UnityEvent<Vector2> OnMovePerform;
    public UnityEvent<Vector2> OnMoveEnd;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Touch.Move.performed += MovePerformHandler;
        inputActions.Touch.Move.canceled += MoveEndHandler;
    }

    private void OnDisable()
    {
        inputActions.Touch.Move.performed -= MovePerformHandler;
        inputActions.Touch.Move.canceled -= MoveEndHandler;
        inputActions.Disable();

    }
    private void MovePerformHandler(InputAction.CallbackContext context)
    {
        OnMovePerform?.Invoke(context.ReadValue<Vector2>());
    }

    private void MoveEndHandler(InputAction.CallbackContext context)
    {
        OnMoveEnd?.Invoke(context.ReadValue<Vector2>());
    }

}
