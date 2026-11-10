using UnityEngine;
using Zenject;

// This class controls the player animations. It uses an animation state machine and factory.
// You can easily add new animations by adding a new AnimState class that implements IAnimState.
// Walk and Idle animations are available for now.
// The animation switches to Walk based on the MovementVector property in PlayerController.cs.

[RequireComponent(typeof(Animator))]
public class PlayerAnimationsController : MonoBehaviour
{
    //Zenject
    [Inject] private InputHandler inputHandler;

    //References
    private Animator animator;
    private IAnimState currentState;
    private AnimationStateFactory stateFactory;

    //Animator Variables
    private bool isWalking;
    private int isWalkingHash;


    public Animator Animator { get => animator; }
    public bool IsWalking { get => isWalking; }
    public int IsWalkingHash { get => isWalkingHash; }


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        isWalkingHash = Animator.StringToHash("isWalking");

        stateFactory = new();
        currentState = stateFactory.IdleState;
        currentState.EnterState(this);
    }

    void Update()
    {
        isWalking = inputHandler.InputVector != Vector2.zero;

        currentState.UpdateState(this, stateFactory);
    }

    public void SwitchAnimState(IAnimState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }
}