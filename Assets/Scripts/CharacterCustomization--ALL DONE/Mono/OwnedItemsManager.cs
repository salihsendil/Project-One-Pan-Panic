using System.Collections.Generic;
using UnityEngine;
using Zenject;

[System.Serializable]
public class OwnedItemsData
{
    public CharacterPartType partType;
    public List<string> itemIDs;

    public OwnedItemsData(CharacterPartType partType, List<string> itemIDs)
    {
        this.partType = partType;
        this.itemIDs = itemIDs;
    }
}

public class OwnedItemsManager : MonoBehaviour
{
    [Inject] private CustomizationSaveManager saveManager;

    private Dictionary<CharacterPartType, HashSet<string>> ownedItems = new();

    private void OnEnable()
    {
        saveManager.OnOwnedItemDataLoaded += SyncOwnedItemsData;
    }

    private void OnDisable()
    {
        saveManager.OnOwnedItemDataLoaded -= SyncOwnedItemsData;
    }

    public bool IsOwnedItem(CharacterPartType partType, string id)
    {
        return ownedItems.TryGetValue(partType, out var set) && set.Contains(id);
    }


    public void AddOwnedItem(CharacterPartType partType, string itemID)
    {
        if (!ownedItems.ContainsKey(partType))
        {
            ownedItems[partType] = new HashSet<string>();
        }

        if (ownedItems.TryGetValue(partType, out var set))
        {
            if (set == null)
            {
                set = new HashSet<string>();
                ownedItems[partType] = set;
            }
            set.Add(itemID);
        }
    }

    private void SyncOwnedItemsData(OwnedItemsDataWrapper dataWrapper)
    {
        if (dataWrapper.ownedItems.Count <= 0) { return; }

        foreach (var data in dataWrapper.ownedItems)
        {
            if (data.itemIDs == null || data.itemIDs.Count == 0) { continue; }

            foreach (var id in data.itemIDs)
            {
                AddOwnedItem(data.partType, id);
                Debug.Log(data.partType + " " + id);
            }
        }
    }
}
