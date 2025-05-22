using UnityEngine;

[CreateAssetMenu(fileName = "Kitchen Object", menuName = "Scriptable Objects/New Kitchen Object")]
public class KitchenObjectSO : ScriptableObject
{
    public string objectName;
    public Sprite objectIcon;
    public GameObject objectPrefab;
    public bool isCuttable;
    public bool isCookable;
}
