using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputHandler : MonoBehaviour
{
    //Zenject
    [Inject] private SignalBus signalBus;

    private PlayerInput playerInput;
    private bool inputEnabled = false;
    private Vector2 inputVector;
    private Vector3 movementVector;

    public Vector3 MovementVector { get => movementVector; }

    public event Action OnInteractionStarted;
    public event Action OnInteractionCanceled;

    public event Action OnInteractionAlternateStarted;
    public event Action OnInteractionAlternateCanceled;

    private void Awake()
    {
        playerInput = new();
    }

    private void OnEnable()
    {
        SubscribeInputActions();

        signalBus.Subscribe<GameStartedSignal>(UnlockInputs);
        signalBus.Subscribe<GameFinishedSignal>(LockInputs);
    }

    private void OnDisable()
    {
        UnsubscribeInputActions();

        signalBus.Unsubscribe<GameStartedSignal>(UnlockInputs);
        signalBus.Unsubscribe<GameFinishedSignal>(LockInputs);
    }

    private void UnlockInputs()
    {
        playerInput.Enable();
        inputEnabled = true;
    }

    private void LockInputs()
    {
        inputVector = Vector2.zero;
        movementVector = Vector3.zero;

        inputEnabled = false;
        playerInput.Disable();
    }

    public void SetInput(bool enabled)
    {
        if (enabled)
        {
            UnlockInputs();
            return;
        }

        LockInputs();
    }

    private void SubscribeInputActions()
    {
        playerInput.Player.Movement.started += Move;
        playerInput.Player.Movement.performed += Move;
        playerInput.Player.Movement.canceled += Move;

        playerInput.Player.Interaction.started += Interaction;
        playerInput.Player.Interaction.canceled += Interaction;

        playerInput.Player.Interaction_Alternate.started += InteractionAlternate;
        playerInput.Player.Interaction_Alternate.canceled += InteractionAlternate;
    }

    private void UnsubscribeInputActions()
    {
        playerInput.Player.Movement.started -= Move;
        playerInput.Player.Movement.performed -= Move;
        playerInput.Player.Movement.canceled -= Move;

        playerInput.Player.Interaction.started -= Interaction;
        playerInput.Player.Interaction.canceled -= Interaction;

        playerInput.Player.Interaction_Alternate.started -= InteractionAlternate;
        playerInput.Player.Interaction_Alternate.canceled -= InteractionAlternate;
    }

    private void Move(InputAction.CallbackContext callback)
    {
        SetMovementInput(callback.ReadValue<Vector2>());
    }

    public void SetMovementInput(Vector2 vector)
    {
        if (!inputEnabled) return;

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

        else if (callback.canceled)
        {
            OnInteractionCanceled?.Invoke();
        }
    }

    private void InteractionAlternate(InputAction.CallbackContext callback)
    {
        if (callback.started)
        {
            Debug.Log("alternate performed");
            OnInteractionAlternateStarted?.Invoke();
        }

        else if (callback.canceled)
        {
            Debug.Log("alternate canceled");
            OnInteractionAlternateCanceled?.Invoke();
        }
    }
}
