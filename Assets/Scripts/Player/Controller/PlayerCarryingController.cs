using UnityEngine;

public class PlayerCarryingController : MonoBehaviour, IItemHolder
{
    [SerializeField] private Transform holdPoint;
    [SerializeField] private GameObject currentItem;
    [SerializeField] private PlayerInteractor playerInteractor;

    private void OnEnable()
    {
        playerInteractor.OnInteractionRequest += TestMethod;
        SetItem(currentItem);
    }

    public bool HasItem() => currentItem != null;
    public GameObject GetItem() { return currentItem; }

    public void SetItem(GameObject obj)
    {
        currentItem = obj;
        currentItem.transform.position = holdPoint.position;
        currentItem.transform.SetParent(holdPoint);
    }

    public GameObject RemoveItem()
    {
        var tempItem = currentItem;
        currentItem = null;
        return tempItem;
    }

    private void TestMethod(IInteractableModule interactable)
    {
        interactable.TryInteract(this);
    }

}
