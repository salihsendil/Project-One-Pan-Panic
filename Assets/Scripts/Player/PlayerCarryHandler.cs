using UnityEngine;

public class PlayerCarryHandler : MonoBehaviour
{
    [SerializeField] private bool hasKitchenObject;
    [SerializeField] private Transform holdPointTransform;
    [SerializeField] private GameObject currentCarryObject;

    public bool HasKitchenObject { get => hasKitchenObject; }
    public GameObject CurrentCarryObject { get => currentCarryObject; }

    void Start()
    {
        BaseKitchenStation.OnObjectPickUpRequest += PickUpKitchenObject;
        BaseKitchenStation.OnObjectDropRequest += DropKitchenObject;
    }

    private void PickUpKitchenObject(GameObject kitchenObject)
    {
        hasKitchenObject = true;
        currentCarryObject = kitchenObject;
        currentCarryObject.transform.position = holdPointTransform.position;
        currentCarryObject.transform.SetParent(holdPointTransform);
    }

    private GameObject DropKitchenObject()
    {
        var temp = currentCarryObject;
        currentCarryObject = null;
        hasKitchenObject = false;
        return temp;        
    }
}
