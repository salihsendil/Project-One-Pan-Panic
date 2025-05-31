using UnityEngine;

public enum KitchenObjectState { Whole, Raw, Chopped, Cooked, Burned }

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO objectData;
    [SerializeField] private KitchenObjectState currentState;

    public KitchenObjectSO ObjectData => objectData;
    public KitchenObjectState CurrentState { get => currentState; set => currentState = value; }

    private void Start()
    {
        currentState = objectData.kitchenObjectState;
    }

    public void UpdateVisual(Mesh newMesh)
    {
        GetComponent<MeshFilter>().mesh = newMesh;
    }
}
