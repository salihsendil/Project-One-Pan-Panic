using UnityEngine;

public class PlayerCarryHandler : MonoBehaviour
{
    [SerializeField] private Transform holdPointTransform;
    [SerializeField] private GameObject currentCarryObj;

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



}
