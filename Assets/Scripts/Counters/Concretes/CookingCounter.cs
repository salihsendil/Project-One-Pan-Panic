using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemInteractionModule))]
[RequireComponent(typeof(CookingModule))]
public class CookingCounter : BaseCounter
{
    private ItemSocket itemSocket;
    private CookingModule cookingModule;

    private void Awake()
    {
        TryGetComponent(out itemSocket);

        TryGetComponent(out ItemInteractionModule itemInteractionModule);
        counterModules[0] = itemInteractionModule;

        TryGetComponent(out cookingModule);
    }

    public override void Interact(PlayerCarryingController player)
    {
        // PLAYER ELİ BOŞ
        if (!player.HasItem())
        {
            // Tezgahta item yoksa hiçbir şey yapma.
            if (!itemSocket.HasItem()) { return; }

            // Üzerindeki item pişirme sürecinde değilse: normal al/bırak davranışı
            // tamamen ItemInteractionModule üzerinden çalışsın.
            if (!cookingModule.IsProcessing())
            {
                base.Interact(player);
                return;
            }

            // Üzerinde pişen bir item varsa, önce pişirmeyi durdur,
            // sonra yine ItemInteractionModule ile item'i oyuncuya ver.
            BaseKitchenItem socketItem = itemSocket.GetItem();
            if (!socketItem.TryGetBehaviourController(out ItemBehaviourController controller)) { return; }

            if (cookingModule.TryInteractPause(controller))
            {
                base.Interact(player);
            }

            return;
        }

        // PLAYER ELİNDE İTEM VAR
        BaseKitchenItem heldItem = player.GetItem();

        // Tezgahta item yoksa: uygun bir ingredient ise otomatik pişirmeyi başlat.
        if (!itemSocket.HasItem())
        {
            if (!heldItem.TryGetBehaviourController(out ItemBehaviourController controller)) { return; }

            if (!cookingModule.CanInteractableAuto(controller)) { return; }

            // Önce normal al/bırak ile item'i tezgaha koy,
            // ardından cooking module üzerinden süreci başlat.
            base.Interact(player);
            cookingModule.InteractAuto(controller);
            return;
        }

        // Tezgahta zaten bir item var ve oyuncu elinde bir CONTAINER (tabak vb.) tutuyor.
        // Burada iki item birleştirilir (örn. pişmiş eti tabağa almak),
        // pişirme varsa durdurulur; al/bırak davranışı yine ItemInteractionModule'de kalır.
        if (heldItem is ContainerItem containerItem)
        {
            BaseKitchenItem socketItem = itemSocket.GetItem();

            if (!containerItem.TryInteractWith(socketItem)) { return; }

            if (!socketItem.TryGetBehaviourController(out ItemBehaviourController controller)) { return; }

            cookingModule.TryInteractPause(controller);
            return;
        }
    }

    public override bool TryGetItemIcon()
    {
        if (!itemSocket.HasItem()) { return false; }

        if (!itemSocket.GetItem().TryGetComponent(out IInfoProvider provider)) { return false; }

        provider.GetDataInfo();

        return true;
    }
}