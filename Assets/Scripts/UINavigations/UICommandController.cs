using UnityEngine;
using Zenject;

public class UICommandController : MonoBehaviour
{
    [Inject] private readonly UICommandFactory commandFactory;

    private void Start()
    {
        commandFactory.Initialize();
    }

    public void GenericCommandInvoked(UICommandSO commandSO)
    {
        var command = GetCommandExecutor(commandSO.CommandType);
        command?.Execute(commandSO);
    }

    private IUICommand GetCommandExecutor(UICommandType type)
    {
        if (commandFactory.commandMap.TryGetValue(type, out IUICommand command))
        {
            return command;
        }

        Debug.LogError("No command found");
        return null;
    }
}
