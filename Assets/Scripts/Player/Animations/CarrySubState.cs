using UnityEngine;

public class CarrySubState : IAnimSubState
{
    public void EnterSubState(PlayerAnimationController animationController)
    {
        animationController.Animator.SetBool(animationController.IsCarryingHash, true);
    }
    public void UpdateSubState(PlayerAnimationController animationController)
    {
        if (!animationController.IsCarrying)
        {
            animationController.SwitchSubState(animationController.StateFactory.EmptySubState);
        }
    }

    public void ExitSubState(PlayerAnimationController animationController)
    {
        animationController.Animator.SetBool(animationController.IsCarryingHash, false);
    }
}
