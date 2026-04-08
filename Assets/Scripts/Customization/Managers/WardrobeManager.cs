using System;
using System.Collections.Generic;
using Zenject;

public class WardrobeManager : ISaveable, IInitializable
{
    //Zenject
    [Inject] private SaveSystem saveSystem;

    //Data
    private Dictionary<BodyPartType, HashSet<string>> ownedCloths = new();
    private Dictionary<BodyPartType, string> equippedCloths = new();

    //Properties
    public SaveDataType GetSaveDataType => SaveDataType.PlayerData;
    public Dictionary<BodyPartType, string> EquippedCloths { get => equippedCloths; }


    public void Initialize()
    {
        LoadData();
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

        SaveData();
    }

    public void Equip(BodyPartType partType, string newID)
    {
        equippedCloths[partType] = newID;

        SaveData();
    }

    #region SaveLoadData

    public void SaveData()
    {
        PlayerDataSave data = saveSystem.GetData<PlayerDataSave>(GetSaveDataType);

        List<OutfitData> newOutfitList = new();

        foreach (var owned in ownedCloths)
        {
            OutfitData outfitData = new();
            outfitData.Key = owned.Key;
            if (!equippedCloths.ContainsKey(owned.Key))
                equippedCloths.Add(owned.Key, ""); //fix required!
            outfitData.EquippedItem = equippedCloths[owned.Key];

            foreach (var cloths in ownedCloths[owned.Key])
                outfitData.OwnedItems.Add(cloths);

            newOutfitList.Add(outfitData);
        }

        data.Outfits = newOutfitList;
        saveSystem.UpdateData(GetSaveDataType, data);
        saveSystem.SaveData(GetSaveDataType);
    }

    public void LoadData()
    {
        PlayerDataSave save = saveSystem.GetData<PlayerDataSave>(GetSaveDataType);

        if (save == null) return;

        foreach (var data in save.Outfits)
        {
            HashSet<string> ownedItems = new();

            foreach (var outfit in data.OwnedItems)
            {
                ownedItems.Add(outfit);
            }

            ownedCloths.Add(data.Key, ownedItems);
            equippedCloths.Add(data.Key, data.EquippedItem);
        }
    }

    #endregion
}