using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputHandler : MonoBehaviour
{
    //Zenject
    [Inject] private SignalBus signalBus;

    private PlayerInput playerInput;
    private Vector2 inputVector;
    private Vector3 movementVector;

    public Vector3 MovementVector { get => movementVector; }

    public event Action OnInteractionButtonPressed;
    public event Action OnInteractionAlternateButtonPressed;

    private void Awake()
    {
        playerInput = new();
    }

    private void OnEnable()
    {
        signalBus.Subscribe<GameStartedSignal>(UnlockInput);
        signalBus.Subscribe<GameFinishedSignal>(LockInput);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<GameStartedSignal>(UnlockInput);
        signalBus.Unsubscribe<GameFinishedSignal>(LockInput);
    }

    private void UnlockInput()
    {
        playerInput.Enable();
        playerInput.Movement.Move.started += Move;
        playerInput.Movement.Move.performed += Move;
        playerInput.Movement.Move.canceled += Move;
        playerInput.Interactions.Interaction.performed += Interaction;
        playerInput.Interactions.InteractionAlternate.performed += InteractionAlternate;
    }

    private void LockInput()
    {
        inputVector = Vector2.zero;
        movementVector = Vector3.zero;

        playerInput.Movement.Move.started -= Move;
        playerInput.Movement.Move.performed -= Move;
        playerInput.Movement.Move.canceled -= Move;
        playerInput.Interactions.Interaction.performed -= Interaction;
        playerInput.Interactions.InteractionAlternate.performed -= InteractionAlternate;
        playerInput.Disable();
    }

    private void Move(InputAction.CallbackContext callback)
    {
        inputVector = callback.ReadValue<Vector2>();
        movementVector = ConvertMovementVector(inputVector);
    }

    private void Interaction(InputAction.CallbackContext callbackContext)
    {
        OnInteractionButtonPressed?.Invoke();
    }

    private void InteractionAlternate(InputAction.CallbackContext callbackContext)
    {
        OnInteractionAlternateButtonPressed?.Invoke();
    }

    private Vector3 ConvertMovementVector(Vector2 input)
    {
        return new Vector3(input.x, 0f, input.y);
    }
}
