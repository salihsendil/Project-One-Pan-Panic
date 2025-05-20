using UnityEngine;

public interface IAnimState 
{
    public void EnterState(PlayerAnimationController animationController);
    public void UpdateState(PlayerAnimationController animationController);
    public void ExitState(PlayerAnimationController animationController);

}
