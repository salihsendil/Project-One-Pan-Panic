using UnityEngine;

public enum UICommandType { LoadScene, Popup, Menu, QuitGame}

public abstract class UICommandSO : ScriptableObject
{
    public abstract UICommandType CommandType {get;}
}
