using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [Header("References")]
    private PlayerInput playerInput;

    [Header("Movement Variables")]
    private Vector2 movementInput;
    public Vector3 MovementVector { get; private set; }

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Player.Movement.started += Movement;
        playerInput.Player.Movement.performed += Movement;
        playerInput.Player.Movement.canceled += Movement;
    }

    private void OnDisable()
    {
        playerInput.Player.Movement.started -= Movement;
        playerInput.Player.Movement.performed -= Movement;
        playerInput.Player.Movement.canceled -= Movement;
        playerInput.Disable();
    }

    private void Movement(InputAction.CallbackContext callback)
    {
        movementInput = callback.ReadValue<Vector2>();
        MovementVector = ConvertMovementVector(movementInput);
    }

    private Vector3 ConvertMovementVector(Vector2 input)
    {
        return new Vector3(input.x, 0f, input.y);
    }

}
