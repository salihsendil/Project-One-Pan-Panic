
public class AnimationStateFactory
{
    private IdleState idleState = new();
    private WalkState walkState = new();

    public WalkState WalkState { get => walkState; }
    public IdleState IdleState { get => idleState; }
}
