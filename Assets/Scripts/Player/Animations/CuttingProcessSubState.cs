using UnityEngine;

public class CuttingProcessSubState : IAnimSubState
{
    public void EnterSubState(PlayerAnimationController animationController)
    {
        animationController.Animator.SetBool(animationController.IsCuttingProcessHash, true);
    }

    public void UpdateSubState(PlayerAnimationController animationController)
    {
        if (!animationController.IsCuttingProcess)
        {
            animationController.SwitchSubState(animationController.StateFactory.EmptySubState);

        }
    }
    public void ExitSubState(PlayerAnimationController animationController)
    {
        animationController.Animator.SetBool(animationController.IsCuttingProcessHash, false);
    }

}
