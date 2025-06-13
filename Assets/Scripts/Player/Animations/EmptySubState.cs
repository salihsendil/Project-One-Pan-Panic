using UnityEngine;

public class EmptySubState : IAnimSubState
{
    public void EnterSubState(PlayerAnimationController animationController) { }
    public void UpdateSubState(PlayerAnimationController animationController)
    {
        if (animationController.IsCarrying && !animationController.IsCuttingProcess)
        {
            animationController.SwitchSubState(animationController.StateFactory.CarrySubState);
        }

        if (animationController.IsCuttingProcess && !animationController.IsCarrying)
        {
            animationController.SwitchSubState(animationController.StateFactory.CuttingProcessSubState);
        }

    }

    public void ExitSubState(PlayerAnimationController animationController) { }

}
