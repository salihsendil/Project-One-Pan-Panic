using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterCustomizer : MonoBehaviour
{
    public Dictionary<BodyPartType, CharacterBodyPart> bodyPartSlots = new();

    public event Action<BodyPartType, string> OnCustomizeChanged;

    public static CharacterCustomizer Instance { get; private set; }

    private void Awake()
    {
        #region SingletonPattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        #endregion

        var parts = GetComponentsInChildren<CharacterBodyPart>().ToList();
        foreach (var part in parts)
        {
            bodyPartSlots[part.BodyPartType] = part;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("i bastým");
            DressUpCharacter(JsonDataHandler.Instance.LoadJson());
        }
    }

    public void ApplyData(CustomizationCategorySlot category, int index)
    {
        CharacterBodyPart targetPart = bodyPartSlots[category.bodyPart];

        targetPart.ApplyMesh(category.category.variants[index].mesh);

        OnCustomizeChanged?.Invoke(targetPart.BodyPartType, category.category.variants[index].id);
        Debug.Log("apply data worked");

    }

    private void DressUpCharacter(HashSet<CustomizeData> customizeData)
    {
        foreach (var parts in bodyPartSlots)
        {
            foreach (var data in customizeData)
            {
                if (parts.Key == data.bodyPart)
                {
                    CustomizationVariantsSO catalog = CustomizationDataManager.Instance.Data.GetCategoryByBodyPart(data.bodyPart);
                    CustomizationVariantItem item = catalog.GetItemById(data.meshId);

                    parts.Value.ApplyMesh(item.mesh);
                    break;
                }
            }
        }
    }


}
