using UnityEngine;

public class EmptySubState : IAnimSubState
{
    public void EnterSubState(PlayerAnimationController animationController) { }
    public void UpdateSubState(PlayerAnimationController animationController)
    {
        if (animationController.IsCarrying)
        {
            animationController.SwitchSubState(animationController.StateFactory.CarrySubState);
        }
    }

    public void ExitSubState(PlayerAnimationController animationController) { }

}
