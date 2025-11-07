using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[System.Serializable]
public class EquipmentVariant
{
    [HideInInspector] public string id;
    public Mesh mesh;
    public int cost;

#if UNITY_EDITOR
    public void EnsureID()
    {
        if (mesh == null)
        {
            id = string.Empty;
            return;
        }

        string path = AssetDatabase.GetAssetPath(mesh);

        if (string.IsNullOrEmpty(path))
        {
            id = string.Empty;
            return;
        }

        id = AssetDatabase.AssetPathToGUID(path);
    }

#endif
}

[CreateAssetMenu(fileName = "New Equipment SO", menuName = "Scriptable Object/Character Customization/New Equipments")]
public class EquipmentSO : ScriptableObject
{
    [SerializeField] private List<EquipmentVariant> variants = new();

    public List<EquipmentVariant> Variants { get => variants; }

#if UNITY_EDITOR
    private void OnValidate()
    {
        foreach (var v in variants)
            v?.EnsureID();

        EditorUtility.SetDirty(this);
    }
#endif
}
