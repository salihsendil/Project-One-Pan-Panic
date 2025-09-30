using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomizationVariantItem
{
    public string id;
    public Mesh mesh;
    public int cost;
}

[CreateAssetMenu(fileName = "New Customization Variants", menuName = "Scriptable Object/Character Customization/New Customization Variants")]

public class CustomizationVariantsSO : ScriptableObject
{
    public List<CustomizationVariantItem> variants = new();

    public CustomizationVariantItem GetItemById(string id)
    {
        for (int i = 0; i < variants.Count; i++)
        {
            if (variants[i].id == id)
            {
                return variants[i];
            }
        }
        return null;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (variants == null) { return; }

        HashSet<string> uniqueIDs = new HashSet<string>();

        for (int i = 0; i < variants.Count; i++)
        {
            var temp = variants[i];

            if (string.IsNullOrEmpty(temp.id) || uniqueIDs.Contains(temp.id))
            {
                temp.id = System.Guid.NewGuid().ToString();
                UnityEditor.EditorUtility.SetDirty(this);
            }
            uniqueIDs.Add(temp.id);
        }
    }
#endif
}
