using UnityEngine;
using Zenject;

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

    public Animator Animator { get => animator;}
    public bool IsWalking { get => isWalking; }
    public int IsWalkingHash { get => isWalkingHash;}

    private void Awake()
    {
        TryGetComponent(out animator);
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
        isWalking = inputHandler.MovementVector != Vector3.zero;
        
        currentState.UpdateState(this, stateFactory);
    }

    public void SwitchAnimState(IAnimState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }
}
