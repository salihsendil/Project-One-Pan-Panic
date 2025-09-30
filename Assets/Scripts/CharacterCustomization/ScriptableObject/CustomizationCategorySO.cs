using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomizationCategorySlot
{
    public BodyPartType bodyPart;
    public CustomizationVariantsSO category;
}


[CreateAssetMenu(fileName ="New Customization Data", menuName = "Scriptable Object/Character Customization/New Customization Data")]
public class CustomizationCategorySO : ScriptableObject
{
    public List<CustomizationCategorySlot> categories = new();

    public CustomizationVariantsSO GetCategoryByBodyPart(BodyPartType bodyPart)
    {
        for (int i = 0; i < categories.Count; i++)
        {
            if (categories[i].bodyPart == bodyPart)
            {
                return categories[i].category;
            }
        }

        return null;
    }

}
