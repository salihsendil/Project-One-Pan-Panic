using UnityEngine;

public class PlayerCarryHandler : MonoBehaviour
{
    [SerializeField] private bool hasKitchenObject;
    [SerializeField] private Transform holdPointTransform;
    [SerializeField] private GameObject currentCarryGameObject;
    [SerializeField] private KitchenObject currentCarryKitchenObject;

    public bool HasKitchenObject { get => hasKitchenObject; }
    public GameObject CurrentCarryGameObject { get => currentCarryGameObject; }
    public KitchenObject CurrentCarryKitchenObject { get => currentCarryGameObject.GetComponent<KitchenObject>(); }

    void Start()
    {
        BaseKitchenStation.OnObjectPickUpRequest += PickUpKitchenObject;
        BaseKitchenStation.OnObjectDropRequest += DropKitchenObject;
    }

    private void PickUpKitchenObject(GameObject kitchenObject)
    {
        hasKitchenObject = true;
        currentCarryGameObject = kitchenObject;
        currentCarryGameObject.transform.position = holdPointTransform.position;
        currentCarryGameObject.transform.SetParent(holdPointTransform);
    }

    private KitchenObject DropKitchenObject()
    {
        var temp = CurrentCarryKitchenObject;
        currentCarryGameObject = null;
        hasKitchenObject = false;
        return temp;
    }
}
