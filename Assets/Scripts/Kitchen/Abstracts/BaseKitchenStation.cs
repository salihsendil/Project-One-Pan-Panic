using System;
using UnityEngine;

public abstract class BaseKitchenStation : MonoBehaviour, IInteractable
{
    public static Func<GameObject> OnObjectDropRequest;
    public static Action<GameObject> OnObjectPickUpRequest;

    [SerializeField] protected GameObject currentKitchenObject;
    [SerializeField] protected Transform kitchenObjectPickUpPoint;
    public virtual bool HasKitchenObject() => currentKitchenObject != null;

    public abstract void Interact(PlayerCarryHandler interactor);
}