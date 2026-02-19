public interface IUIAction
{
    public void Execute();
}

public interface IUIAction<T> : IUIAction
{
    public void Execute(T param);
}