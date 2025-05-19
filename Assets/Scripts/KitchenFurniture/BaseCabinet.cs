using System;
using UnityEngine;

public abstract class BaseCabinet : MonoBehaviour, IInteractables
{
    public static event Action<GameObject> OnCabinetInteractionRequested;

    [SerializeField] protected KitchenObjectSO cabinetSO;

    public void Interact()
    {
        GameObject gameObject = Instantiate(cabinetSO.objectPrefab, transform.position, Quaternion.identity);
        OnCabinetInteractionRequested?.Invoke(gameObject);
    }
}
