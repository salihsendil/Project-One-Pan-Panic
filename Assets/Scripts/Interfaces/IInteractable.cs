using UnityEngine;

public interface IInteractable<T>
{
    public void Interact(T data);
}
