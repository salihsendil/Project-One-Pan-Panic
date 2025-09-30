using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomizeData
{
    public BodyPartType bodyPart;
    public string meshId;

    public CustomizeData(BodyPartType bodyPart, string meshId)
    {
        this.bodyPart = bodyPart;
        this.meshId = meshId;
    }
}

public class CharacterCustomizationState : MonoBehaviour
{
    private HashSet<CustomizeData> customizationHashList = new HashSet<CustomizeData>();

    public HashSet<CustomizeData> CustomizationHashList { get => customizationHashList; }

    //equip yapýlýnca datayý güncellicez
    //en son menüden çýkýlýnca json alýcaz


    public static CharacterCustomizationState Instance { get; private set; }

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

    }
    private void Start()
    {
        CharacterCustomizer.Instance.OnCustomizeChanged += UpdateHashList;
    }

    private void OnDisable()
    {
        CharacterCustomizer.Instance.OnCustomizeChanged -= UpdateHashList;
    }


    private void UpdateHashList(BodyPartType bodyPart, string newId)
    {
        foreach (var data in customizationHashList)
        {
            if (data.bodyPart == bodyPart)
            {
                data.meshId = newId;
                return;
            }
        }

        AddDataToHashList(bodyPart, newId);
    }

    private void AddDataToHashList(BodyPartType bodyPart, string newId)
    {
        CustomizeData newData = new CustomizeData(bodyPart, newId);
        customizationHashList.Add(newData);
    }





























    //private void Start()
    //{
    //    SetHashData(CharacterCustomizer.Instance.bodyPartSlots);
    //}

    //private void SetHashData(Dictionary<BodyPartType, CharacterBodyPart> customizeDict) //ilk datayý oluþturma
    //{
    //    foreach (var item in customizeDict)
    //    {
    //        CustomizeData data = new();
    //        data.bodyPart = item.Key;
    //        customizationHashList.Add(data);
    //    }
    //}

    //private CustomizeData GetCustomizeData(BodyPartType bodyPart)
    //{
    //    foreach (var item in customizationHashList)
    //    {
    //        if (item.bodyPart == bodyPart)
    //        {
    //            return item;
    //        }
    //    }
    //    return null;
    //}

    //private void UpdateHashData(BodyPartType bodyPart, string newId)
    //{
    //    Debug.Log("update hash data worked");

    //    CustomizeData customizeData = GetCustomizeData(bodyPart);

    //    if (customizeData != null)
    //    {
    //        customizeData.meshId = newId;
    //        return;
    //    }

    //    else
    //    {
    //        customizeData.bodyPart = bodyPart;
    //        customizeData.meshId = newId;
    //        AddHashData(customizeData);
    //    }
    //}

    //private void AddHashData(CustomizeData newData)
    //{
    //    customizationHashList.Add(newData);
    //}


}

