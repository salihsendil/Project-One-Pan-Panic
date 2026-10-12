using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

// This class detects inputs using the New Input System (PlayerInput.cs and input mapping).
// It supports 4-way walking, instant interaction with the E key, and hold interaction with the F key for now.
//
// There is a getter method for movement and events for interactions.
// The movement vector is not converted (X,Y to X,Z) in this class. I mean, it is not converted from 
//
// You can extend it easily. Just add a new action to PlayerInput.cs, then subscribe to these actions
// in the OnEnable and OnDisable methods, create new methods for the new actions, and you're done.


public class InputHandler : MonoBehaviour
{
    //[Inject] private SignalBus signalBus;

    private PlayerInput playerInput;

    private Vector2 inputVector;

    public Vector2 InputVector => inputVector;


    public event Action OnInteractInstantPerformed;

    public event Action OnInteractHoldStarted;
    public event Action OnInteractHoldCanceled;


    private void Awake()
    {
        playerInput = new();
        //playerInput.Disable();
        playerInput.Enable();
    }

    private void OnEnable()
    {
        //signalBus.Subscribe<GamePhaseChangedSignal>(HandleGamePhaseChange);

        playerInput.Player.Movement.started += Move;
        playerInput.Player.Movement.performed += Move;
        playerInput.Player.Movement.canceled += Move;

        playerInput.Player.Interact_Instant.performed += InteractInstant;

        playerInput.Player.Interact_Hold.started += InteractHoldStarted;
        playerInput.Player.Interact_Hold.canceled += InteractHoldCanceled;
    }

    private void OnDisable()
    {
        //signalBus.Unsubscribe<GamePhaseChangedSignal>(HandleGamePhaseChange);

        playerInput.Player.Movement.started -= Move;
        playerInput.Player.Movement.performed -= Move;
        playerInput.Player.Movement.canceled -= Move;

        playerInput.Player.Interact_Instant.performed -= InteractInstant;

        playerInput.Player.Interact_Hold.started -= InteractHoldStarted;
        playerInput.Player.Interact_Hold.canceled -= InteractHoldCanceled;
    }

    private void HandleGamePhaseChange(GamePhaseChangedSignal signal)
    {
        switch (signal.Phase)
        {
            case GamePhase.Play:
                playerInput.Enable();
                break;

            default:
                inputVector = Vector2.zero;
                OnInteractHoldCanceled?.Invoke();
                playerInput.Disable();
                break;
        }
    }

    private void Move(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }

    private void InteractInstant(InputAction.CallbackContext context)
    {
        OnInteractInstantPerformed?.Invoke();
    }

    private void InteractHoldStarted(InputAction.CallbackContext context)
    {
        OnInteractHoldStarted?.Invoke();
    }

    private void InteractHoldCanceled(InputAction.CallbackContext context)
    {
        OnInteractHoldCanceled?.Invoke();
    }
}