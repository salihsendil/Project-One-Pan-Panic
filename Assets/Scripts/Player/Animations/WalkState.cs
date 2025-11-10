using UnityEngine;

public class WalkState : IAnimState
{
    public void EnterState(PlayerAnimationsController controller)
    {
        controller.Animator.SetBool(controller.IsWalkingHash, controller.IsWalking);
    }
    public void UpdateState(PlayerAnimationsController controller, AnimationStateFactory stateFactory)
    {
        if (!controller.IsWalking)
        {
            controller.SwitchAnimState(stateFactory.IdleState);
        }
    }

    public void ExitState(PlayerAnimationsController controller)
    {
    }

}
