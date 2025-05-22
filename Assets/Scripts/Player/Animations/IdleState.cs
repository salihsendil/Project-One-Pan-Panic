using UnityEngine;

public class IdleState : IAnimState
{
    public void EnterState(PlayerAnimationController animationController)
    {
        
    }
    public void UpdateState(PlayerAnimationController animationController)
    {
        if (animationController.IsRunning)
        {
            animationController.SwitchState(animationController.StateFactory.RunState);
        }
    }

    public void ExitState(PlayerAnimationController animationController)
    {
        
    }
}
