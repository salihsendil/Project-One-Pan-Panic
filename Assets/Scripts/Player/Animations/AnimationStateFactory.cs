
public class AnimationStateFactory
{
    private readonly IdleState idleState = new();
    private readonly WalkState walkState = new();

    public WalkState WalkState { get => walkState; }
    public IdleState IdleState { get => idleState; }
}
