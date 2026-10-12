
// Creates and stores animation states using the Factory pattern to avoid allocations
// and unnecessary garbage collection on every animation switch.

public class AnimationStateFactory
{
    private readonly IdleState idleState = new();
    private readonly WalkState walkState = new();

    public WalkState WalkState { get => walkState; }
    public IdleState IdleState { get => idleState; }
}
