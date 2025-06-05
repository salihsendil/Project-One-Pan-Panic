using UnityEngine;

[CreateAssetMenu(fileName = "New Kitchem Item", menuName = "Scriptable Object/New Kitchen Item Data")]
public class KitchenItemSO : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public GameObject Prefab;
    [Tooltip("How many seconds does it take to process the item?")]
    public float processTime;
}
