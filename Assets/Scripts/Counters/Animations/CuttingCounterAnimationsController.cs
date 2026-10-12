using UnityEngine;


// This is a simple class for controlling the Cutting Counter's knife or other animations.


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
        animator = GetComponent<Animator>();
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
