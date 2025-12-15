using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CuttingCounterAnimationsController : MonoBehaviour
{
    //References
    private Animator animator;

    //Animator Variables
    private bool isCutting;
    private int isCuttingHash;

    private void Awake()
    {
        TryGetComponent(out animator);
    }

    private void Start()
    {
        isCuttingHash = Animator.StringToHash("isCutting");
    }

    public void UpdateAnimationState(bool value)
    {
        isCutting = value;
        animator.SetBool(isCuttingHash, isCutting);
    }
}
