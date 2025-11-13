using UnityEngine;

[CreateAssetMenu(fileName = "New KitchenItemSO", menuName = "Scriptable Objects/New KitchenItemSO")]
public class KitchenItemSO : ScriptableObject
{
    public KitchemItemType kitchemItemType;
    public GameObject prefab;
    public Sprite icon;
}
