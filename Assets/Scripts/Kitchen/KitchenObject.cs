using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO objectData;
    public KitchenObjectSO ObjectData => objectData;
}
