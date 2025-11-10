using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Vector2 inputVector;
    private Vector3 movementVector;

    public Vector3 MovementVector { get => movementVector; }

    private void Awake()
    {
        playerInput = new();
    }

    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Movement.Move.started += Move;
        playerInput.Movement.Move.performed += Move;
        playerInput.Movement.Move.canceled += Move;
    }

    private void OnDisable()
    {
        playerInput.Movement.Move.started -= Move;
        playerInput.Movement.Move.performed -= Move;
        playerInput.Movement.Move.canceled -= Move;
        playerInput.Disable();
    }

    private void Move(InputAction.CallbackContext callback)
    {
        inputVector = callback.ReadValue<Vector2>();
        movementVector = ConvertMovementVector(inputVector);
    }

    private Vector3 ConvertMovementVector(Vector2 input)
    {
        return new Vector3(input.x, 0f, input.y);
    }
}
