using UnityEngine;
using Zenject;

public class PurchaseManager : MonoBehaviour
{
    [Inject] private CustomizationUIController customizationUI;
    [Inject] private OwnedItemsManager ownedItemsManager;
    [Inject] private CurrencyManager currencyManager;

    private void OnEnable()
    {
        customizationUI.OnPurchaseRequested += PurchaseItem;
    }

    private void OnDisable()
    {
        customizationUI.OnPurchaseRequested -= PurchaseItem;
    }


    public void PurchaseItem(CharacterPartType partType, string itemID, int cost)
    {
        if (currencyManager.HasEnoughMoney(cost))
        {
            currencyManager.AddGold(-cost);
            ownedItemsManager.AddOwnedItem(partType, itemID);
        }
    }
}
