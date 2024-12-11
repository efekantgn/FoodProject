using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private PlayerInputActions inputActions;

    public UnityEvent<Vector2> OnMovePerform;
    public UnityEvent<Vector2> OnMoveEnd;

    public Action<Vector2> OnTouch;


    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Touch.Move.performed += MovePerformHandler;
        inputActions.Touch.Move.canceled += MoveEndHandler;
        inputActions.Touch.TouchPos.performed += TouchPerformed;
    }

    private void TouchPerformed(InputAction.CallbackContext context)
    {
        OnTouch?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnDisable()
    {
        inputActions.Touch.Move.performed -= MovePerformHandler;
        inputActions.Touch.Move.canceled -= MoveEndHandler;
        inputActions.Touch.TouchPos.performed -= TouchPerformed;

        inputActions.Disable();

    }
    private void MovePerformHandler(InputAction.CallbackContext context)
    {
        OnMovePerform?.Invoke(context.ReadValue<Vector2>());
    }

    private void MoveEndHandler(InputAction.CallbackContext context)
    {
        OnMoveEnd?.Invoke(Vector2.zero);
    }


}
