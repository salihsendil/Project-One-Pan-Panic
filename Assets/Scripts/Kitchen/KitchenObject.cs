using UnityEngine;

public enum KitchenObjectState { Whole, Raw, Chopped, Cooked, Burned }

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO objectData;
    [SerializeField] private KitchenObjectState currentState = KitchenObjectState.Whole;

    public KitchenObjectSO ObjectData => objectData;
    public KitchenObjectState CurrentState { get => currentState; set => currentState = value; }

    public void UpdateVisual(Mesh newMesh)
    {
        GetComponent<MeshFilter>().mesh = newMesh;
    }
}
