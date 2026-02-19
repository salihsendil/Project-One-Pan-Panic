using UnityEngine;

public abstract class BaseUIAction : MonoBehaviour, IUIAction
{
    public abstract void Execute();
}

public abstract class BaseUIAction<T> : MonoBehaviour, IUIAction<T>
{
    public abstract void Execute(T param);

    void IUIAction.Execute() { }
}