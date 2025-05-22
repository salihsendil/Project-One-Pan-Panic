using UnityEngine;

public class AnimationStateFactory
{
    public readonly IdleState IdleState = new IdleState();
    public readonly RunState RunState = new RunState();
    public readonly EmptySubState EmptySubState = new EmptySubState();
    public readonly CarrySubState CarrySubState = new CarrySubState();
}
