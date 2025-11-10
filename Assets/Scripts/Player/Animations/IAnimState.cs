using UnityEngine;

public interface IAnimState
{
    public void EnterState(PlayerAnimationsController controller);
    public void UpdateState(PlayerAnimationsController controller, AnimationStateFactory stateFactory);
    public void ExitState(PlayerAnimationsController controller);
}
