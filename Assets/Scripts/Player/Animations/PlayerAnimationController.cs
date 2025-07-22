using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerCarryHandler))]
public class PlayerAnimationController : MonoBehaviour
{
    [Header("References")]
    private PlayerController playerController;
    private PlayerCarryHandler playerCarryHandler;
    private Rigidbody rb;
    private Animator animator;
    private IAnimState currentState;
    private IAnimSubState currentSubState;
    private AnimationStateFactory stateFactory = new AnimationStateFactory();

    [Header("Animation")]
    private bool isRunning;
    private int isRunningHash;
    private bool isCarrying;
    private int isCarryingHash;
    private bool isCuttingProcess;
    private int isCuttingProcessHash;

    public Animator Animator { get => animator; }
    public bool IsRunning { get => playerController.IsMoving; }
    public int IsRunningHash { get => isRunningHash; }
    public bool IsCarrying { get => playerCarryHandler.HasKitchenItem; }
    public int IsCarryingHash { get => isCarryingHash; }
    public bool IsCuttingProcess { get => playerCarryHandler.HasBusyForProcess; }
    public int IsCuttingProcessHash { get => isCuttingProcessHash; }
    public AnimationStateFactory StateFactory { get => stateFactory; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        playerCarryHandler = GetComponent<PlayerCarryHandler>();

        currentState = stateFactory.IdleState;
        currentState.EnterState(this);

        currentSubState = stateFactory.EmptySubState;
        currentSubState.EnterSubState(this);

        isRunningHash = Animator.StringToHash("isRunning");
        isCarryingHash = Animator.StringToHash("isCarrying");
        isCuttingProcessHash = Animator.StringToHash("isCuttingProcess");
    }

    void Update()
    {
        currentState.UpdateState(this);
        currentSubState.UpdateSubState(this);
    }

    public void SwitchState(IAnimState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public void SwitchSubState(IAnimSubState newSubState)
    {
        currentSubState.ExitSubState(this);
        currentSubState = newSubState;
        currentSubState.EnterSubState(this);
    }
}
