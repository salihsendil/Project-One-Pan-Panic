using UnityEngine;

public class PlayerCarryHandler : MonoBehaviour
{
    [SerializeField] private bool isCarrying;
    [SerializeField] private Transform holdPointTransform;
    [SerializeField] private GameObject currentCarryObj;

    public bool IsCarrying { get => isCarrying; }

    void Start()
    {
        BaseCabinet.OnCabinetInteractionRequested += HoldSpawnObject;
    }

    void Update()
    {
        
    }

    private void HoldSpawnObject(GameObject gameObject)
    {
        currentCarryObj = gameObject;
        //currentCarryObj.transform = holdPointTransform;
    }

    private void SetCarriedObject(GameObject gameObject)
    {

    }

    private void GetCarriedObject()
    {

    }

    private void DropCarriedObject()
    {

    }



}
