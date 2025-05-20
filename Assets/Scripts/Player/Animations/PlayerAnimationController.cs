using UnityEngine;
using Zenject;

public class PlayerAnimationController : MonoBehaviour
{
    [Header("References")]
    private PlayerController playerController;
    private Rigidbody rb;
    private Animator animator;
    private IAnimState currentState;

    [Header("Animation")]
    private bool isRunning;
    private int isRunningHash;

    public Animator Animator { get => animator;}
    public bool IsRunning { get => playerController.CanMove; }
    public int IsRunningHash { get => isRunningHash; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();

        currentState = new IdleState();
        currentState.EnterState(this);

        isRunningHash = Animator.StringToHash("isRunning");
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(IAnimState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }
}
