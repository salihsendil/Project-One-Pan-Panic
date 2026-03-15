using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Zenject;

public class WardrobeManager : ISaveable, IInitializable, IDisposable
{
    [Inject] private SaveSystem saveSystem;

    private Dictionary<BodyPartType, HashSet<string>> ownedCloths = new();
    private Dictionary<BodyPartType, string> equippedCloths = new();

    public SaveDataType GetSaveDataType => SaveDataType.Wardrobe;

    public Dictionary<BodyPartType, string> EquippedCloths { get => equippedCloths; }

    public void Initialize()
    {
        saveSystem.Register(this);
    }

    public void Dispose()
    {
        saveSystem.Unregister(this);
    }

    public bool HasCloth(BodyPartType bodyPart, string id)
    {
        if (!ownedCloths.TryGetValue(bodyPart, out HashSet<string> cloths)) { return false; }

        return cloths.Contains(id);
    }

    public bool IsEquipped(BodyPartType partType, string id)
    {
        return equippedCloths.TryGetValue(partType, out var equippedId) && equippedId == id;
    }

    public void Unlock(BodyPartType bodyPart, string id)
    {
        HashSet<string> clothes;
        if (!ownedCloths.TryGetValue(bodyPart, out clothes))
        {
            clothes = new();
        }

        clothes.Add(id);
        ownedCloths[bodyPart] = clothes;
    }

    public void Equip(BodyPartType partType, string newID)
    {
        equippedCloths[partType] = newID;
    }

    public string GetSaveData()
    {
        List<WardrobeSaveData> clothes = new();

        foreach (var owned in ownedCloths)
        {
            WardrobeSaveData saveData = new();
            saveData.Key = owned.Key;
            saveData.EquippedItem = equippedCloths[owned.Key];

            foreach (var cloths in ownedCloths[saveData.Key])
            {
                saveData.OwnedItems.Add(cloths);
            }
            clothes.Add(saveData);
        }

        return JsonConvert.SerializeObject(clothes, Formatting.Indented);
    }

    public void LoadData(string json)
    {
        List<WardrobeSaveData> dataList = JsonConvert.DeserializeObject<List<WardrobeSaveData>>(json);

        foreach (var data in dataList)
        {
            ownedCloths.Add(data.Key, new HashSet<string>());
            equippedCloths[data.Key] = data.EquippedItem;

            foreach (var cloth in data.OwnedItems)
            {
                ownedCloths[data.Key].Add(cloth);
            }
        }
    }
}