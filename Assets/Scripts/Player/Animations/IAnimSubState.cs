using UnityEngine;

public interface IAnimSubState
{
    public void EnterSubState(PlayerAnimationController animationController);
    public void UpdateSubState(PlayerAnimationController animationController);
    public void ExitSubState(PlayerAnimationController animationController);
}
