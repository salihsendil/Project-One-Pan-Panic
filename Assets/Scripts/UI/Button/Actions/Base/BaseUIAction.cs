using UnityEngine;

[RequireComponent(typeof(UIButtonHandler))]
public abstract class BaseUIAction : MonoBehaviour
{
    public abstract void Execute();
}
