
public interface IUICommand
{
    void Execute(UICommandSO parameter);
}

public interface IUICommand<T> : IUICommand where T : UICommandSO
{
    void ExecuteCommand(T parameter);
}
