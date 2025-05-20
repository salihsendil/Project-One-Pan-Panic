using UnityEngine;

public class RunState : IAnimState
{
    public void EnterState(PlayerAnimationController animationController)
    {
        animationController.Animator.SetBool(animationController.IsRunningHash, true);
    }
    public void UpdateState(PlayerAnimationController animationController)
    {
        if (!animationController.IsRunning)
        {
            animationController.SwitchState(new IdleState());
        }
    }

    public void ExitState(PlayerAnimationController animationController)
    {
        animationController.Animator.SetBool(animationController.IsRunningHash, false);
    }
}
