using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static event Action OnInteractionsKeyPressed;

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
        playerInput.Player.Interactions.performed += PlayerInteractions;
    }

    private void OnDisable()
    {
        playerInput.Player.Movement.started -= Movement;
        playerInput.Player.Movement.performed -= Movement;
        playerInput.Player.Movement.canceled -= Movement;
        playerInput.Player.Interactions.performed -= PlayerInteractions;
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

    private void PlayerInteractions(InputAction.CallbackContext callback)
    {
        OnInteractionsKeyPressed?.Invoke();
    }
}
