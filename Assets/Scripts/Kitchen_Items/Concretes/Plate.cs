using System;
using System.Collections.Generic;
using UnityEngine;


// Created for the Plate object.
// Plates can receive items, import items from other containers, and export items to other containers.
//
// There are some requirements for these transfers:
// - When placing an ingredient directly on the plate, it must be ready or processed.
// - The same requirement applies when importing an ingredient from another container.
// - When exporting an ingredient, the plate does not check any requirements.
//   The receiving container is responsible for deciding whether it can accept the ingredient.


public class Plate : ContainerItem, IPoolable
{

    public GameObject GameObject => gameObject;

    /// <summary>
    /// Invoked when an item is added to containers.
    /// </summary>
    /// <remarks>
    /// Parameters: target container, transferred item.
    /// </remarks>
    public event Action<ContainerItem, IPickable> OnItemAddedToPlate;

    public override bool CanReceive(IPickable pickable)
    {
        if (containerSlots.Count >= containerSlots.Capacity) return false;
        if (!pickable.Transform.TryGetComponent(out IngredientItem ingredient)) return false;

        foreach (var slot in containerSlots)
        {
            if (slot.IngredientItem.IngredientItemSO == ingredient.IngredientItemSO) return false;
        }

        if (ingredient.PreparationStage == PreparationStage.Raw || ingredient.PreparationStage == PreparationStage.Burnt) return false;

        return true;
    }

    public override void Receive(IPickable pickable)
    {
        if (!pickable.Transform.TryGetComponent(out IngredientItem ingredientItem)) return;

        ContainerSlot slot = new(ingredientItem);
        containerSlots.Add(slot);
        SetItem(pickable);

        ingredientItem.IconDisplay.Hide();
        iconDisplay.Set(ingredientItem.IngredientItemSO.Icon);
        iconDisplay.Show();
        OnItemAddedToPlate?.Invoke(this, ingredientItem);
    }



    public override bool CanImportItems(List<ContainerSlot> incomingContainerSlots)
    {
        if (incomingContainerSlots.Count + containerSlots.Count > containerSlots.Capacity) return false;
        foreach (var item in incomingContainerSlots)
        {
            if (item.IngredientItem == null) return false;
            if (item.IngredientItem.PreparationStage == PreparationStage.Raw) return false;
            if (item.IngredientItem.PreparationStage == PreparationStage.Burnt) return false;
        }


        var tempList = new List<ContainerSlot>(containerSlots);
        tempList.AddRange(incomingContainerSlots);
        if (true)
        {
            //recipe checker if is not any meal return false
        }



        return true;
    }

    public override void ImportItems(List<ContainerSlot> items)
    {
        foreach (var item in items)
        {
            containerSlots.Add(item);
            SetItem(item.IngredientItem);
            iconDisplay.Set(item.IngredientItem.IngredientItemSO.Icon);
            //check mesh or change mesh
        }

        iconDisplay.Show();
    }



    public override bool CanExportItems()
    {
        return true;
    }

    public override List<ContainerSlot> ExportItems()
    {
        var temp = new List<ContainerSlot>(containerSlots);
        containerSlots.Clear();
        iconDisplay.Hide();
        return temp;
    }

    public void Spawn()
    {

    }

    public void Despawn()
    {
        foreach (var slot in containerSlots)
            slot.IngredientItem.Despawn();

        containerSlots.Clear();
        iconDisplay.Hide();
    }
}