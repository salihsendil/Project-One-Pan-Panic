using System;
using UnityEngine;

public enum KitchenStationType { EmptyCabinet, IngredientCabinet, CuttingCabinet, Stove, TrashBin }

public abstract class BaseKitchenStation : MonoBehaviour, IInteractable
{
    public static Func<KitchenObject> OnObjectDropRequest;
    public static Action<GameObject> OnObjectPickUpRequest;

    [SerializeField] protected abstract KitchenStationType kitchenStationType { get; }
    [SerializeField] protected KitchenObject currentKitchenObject;
    [SerializeField] protected Transform kitchenObjectPickUpPoint;
    [SerializeField] protected MeshRenderer meshRenderer => GetComponent<MeshRenderer>();
    [SerializeField] protected Material baseMaterial;
    [SerializeField] protected Material selectedMaterial;
    public virtual bool HasKitchenObject() => currentKitchenObject != null;

    public abstract void Interact(PlayerCarryHandler interactor);

    private void Start()
    {
        baseMaterial = meshRenderer.material;
    }

    protected void SetCurrentObject()
    {
        currentKitchenObject = OnObjectDropRequest?.Invoke();
        currentKitchenObject.transform.position = kitchenObjectPickUpPoint.position;
        currentKitchenObject.transform.SetParent(kitchenObjectPickUpPoint);
    }

    protected void RemoveCurrentObject()
    {
        OnObjectPickUpRequest?.Invoke(currentKitchenObject.gameObject);
        currentKitchenObject = null;
    }

    public void HandleRayHit(bool isHit)
    {
        meshRenderer.material = isHit ? selectedMaterial : baseMaterial;
    }

    protected void ProcessIngredient()
    {
        KitchenObjectSO.ProcessRule processRule = currentKitchenObject.ObjectData.GetMatch(kitchenStationType, currentKitchenObject.CurrentState);
        currentKitchenObject.UpdateVisual(processRule.outputKitchenObjectMesh);
        currentKitchenObject.CurrentState = processRule.outputKitchenObjectState;
    }

}