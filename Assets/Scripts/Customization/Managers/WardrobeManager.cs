using System;
using System.Collections.Generic;
using Zenject;

public class WardrobeManager : ISaveable, IInitializable
{
    private struct WardrobeCollection
    {
        public string EquippedClothId;
        public HashSet<string> OwnedClothsId;

        public static WardrobeCollection CreateEmpty()
        {
            string defaultId = "default";
            return new WardrobeCollection
            {
                EquippedClothId = defaultId,
                OwnedClothsId = new HashSet<string> { defaultId }
            };
        }

        public WardrobeCollection(string equippedClothId, HashSet<string> ownedClothsId)
        {
            EquippedClothId = equippedClothId;
            OwnedClothsId = ownedClothsId;
        }
    }

    //Zenject
    [Inject] private SaveSystem saveSystem;

    private Dictionary<BodyPartType, WardrobeCollection> wardrobeCatalog = new();

    //Properties
    public SaveDataType GetSaveDataType => SaveDataType.PlayerData;

    public void Initialize()
    {
        LoadData();
    }

    public string GetEquippedClothId(BodyPartType bodyPart)
    {
        if (!wardrobeCatalog.ContainsKey(bodyPart)) return null;

        return wardrobeCatalog[bodyPart].EquippedClothId;
    }

    public bool HasCloth(BodyPartType bodyPart, string id)
    {
        if (!wardrobeCatalog.ContainsKey(bodyPart)) return false;

        HashSet<string> ownedClothes = wardrobeCatalog[bodyPart].OwnedClothsId;

        return ownedClothes.Contains(id);
    }

    public bool IsEquipped(BodyPartType bodyPart, string id)
    {
        if (!wardrobeCatalog.ContainsKey(bodyPart)) return false;

        return wardrobeCatalog[bodyPart].EquippedClothId == id;
    }

    public void Unlock(BodyPartType bodyPart, string id)
    {
        if (!wardrobeCatalog.ContainsKey(bodyPart))
            wardrobeCatalog[bodyPart] = WardrobeCollection.CreateEmpty();
        wardrobeCatalog[bodyPart].OwnedClothsId.Add(id);

        SaveData();
    }

    public void Equip(BodyPartType bodyPart, string newID)
    {
        if (!wardrobeCatalog.ContainsKey(bodyPart)) return;

        WardrobeCollection collection = wardrobeCatalog[bodyPart];
        collection.EquippedClothId = newID;
        wardrobeCatalog[bodyPart] = collection;

        SaveData();
    }

    #region SaveLoadData

    public void SaveData()
    {
        PlayerDataSave data = saveSystem.TryGetData<PlayerDataSave>(GetSaveDataType);
        if (data == null) data = new();
        data.Outfits.Clear();

        foreach (var collection in wardrobeCatalog.Keys)
        {
            OutfitData outfitData = data.Outfits.Find(x => x.Key == collection);
            if (outfitData == null) outfitData = new(collection);
            outfitData.OwnedItems.Clear();

            HashSet<string> ownedCloths = wardrobeCatalog[collection].OwnedClothsId;
            foreach (var owned in ownedCloths)
            {
                outfitData.OwnedItems.Add(owned);
            }

            outfitData.EquippedItem = wardrobeCatalog[collection].EquippedClothId;
            data.Outfits.Add(outfitData);
        }
        saveSystem.UpdateData(GetSaveDataType, data);
        saveSystem.SaveData(GetSaveDataType);
    }

    public void LoadData()
    {
        PlayerDataSave save = saveSystem.TryGetData<PlayerDataSave>(GetSaveDataType);

        //if save null create default data and return
        if (save.Outfits.Count <= 0)
        {
            var parts = Enum.GetValues(typeof(BodyPartType));
            foreach (BodyPartType part in parts)
            {
                UnityEngine.Debug.Log("3");
                WardrobeCollection newCollection = WardrobeCollection.CreateEmpty();
                wardrobeCatalog[part] = newCollection;
            }

            SaveData();
            return;
        }

        //if save found and valid initialize data mapping
        foreach (var data in save.Outfits)
        {
            WardrobeCollection newCollection = WardrobeCollection.CreateEmpty();

            foreach (var owned in data.OwnedItems)
                newCollection.OwnedClothsId.Add(owned);

            newCollection.EquippedClothId = data.EquippedItem;
            wardrobeCatalog[data.Key] = newCollection;
        }
    }

    #endregion
}