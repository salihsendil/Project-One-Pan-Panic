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

    public event Action OnInteractionStarted;
    public event Action OnInteractionPerformed;
    public event Action OnInteractionCanceled;

    private void Awake()
    {
        playerInput = new();
    }

    private void OnEnable()
    {
        //signalBus.Subscribe<GameStartedSignal>(UnlockInput);
        //signalBus.Subscribe<GameFinishedSignal>(LockInput);
        UnlockInput();
    }

    private void OnDisable()
    {
        //signalBus.Unsubscribe<GameStartedSignal>(UnlockInput);
        //signalBus.Unsubscribe<GameFinishedSignal>(LockInput);
    }

    private void UnlockInput()
    {
        playerInput.Enable();
        playerInput.Player.Movement.started += Move;
        playerInput.Player.Movement.performed += Move;
        playerInput.Player.Movement.canceled += Move;
        playerInput.Player.Interaction.started += Interaction;
        playerInput.Player.Interaction.performed += Interaction;
        playerInput.Player.Interaction.canceled += Interaction;
    }

    private void LockInput()
    {
        inputVector = Vector2.zero;
        movementVector = Vector3.zero;

        playerInput.Player.Movement.started -= Move;
        playerInput.Player.Movement.performed -= Move;
        playerInput.Player.Movement.canceled -= Move;
        playerInput.Player.Interaction.started -= Interaction;
        playerInput.Player.Interaction.performed -= Interaction;
        playerInput.Player.Interaction.canceled -= Interaction;
        playerInput.Disable();
    }

    private void Move(InputAction.CallbackContext callback)
    {
        SetMovementInput(callback.ReadValue<Vector2>());
    }

    public void SetMovementInput(Vector2 vector)
    {
        movementVector = ConvertMovementVector(vector);
    }

    private Vector3 ConvertMovementVector(Vector2 input)
    {
        return new Vector3(input.x, 0f, input.y);
    }

    private void Interaction(InputAction.CallbackContext callback)
    {
        if (callback.started)
        {
            OnInteractionStarted?.Invoke();
        }

        else if (callback.performed)
        {
            OnInteractionPerformed?.Invoke();
        }

        else
        {
            OnInteractionCanceled?.Invoke();
        }

    }
}
