using UnityEngine;

public class IdleState : IAnimState
{
    public void EnterState(PlayerAnimationsController controller)
    {
        controller.Animator.SetBool(controller.IsWalkingHash, controller.IsWalking);
    }

    public void UpdateState(PlayerAnimationsController controller, AnimationStateFactory stateFactory)
    {
        if (controller.IsWalking)
        {
            controller.SwitchAnimState(stateFactory.WalkState);
        }
    }

    public void ExitState(PlayerAnimationsController controller)
    {
    }
}
