using UnityEngine;

[CreateAssetMenu(fileName = "Kitchen Object", menuName = "Scriptable Objects/New Kitchen Object")]
public class KitchenObjectSO : ScriptableObject
{
    public GameObject objectPrefab;
    public Sprite objectVisual;
    public string objectName;
}
