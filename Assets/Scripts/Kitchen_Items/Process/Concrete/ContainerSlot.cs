using System;


// This class is used by ContainerItems and stores some references to the Ingredient Items added to the container.


[Serializable]
public class ContainerSlot
{
    public IngredientItem IngredientItem;
    public IngredientProcessModule ProcessModule;
    public ProcessSO ProcessSO;

    public ContainerSlot(IngredientItem ingredientItem)
    {
        IngredientItem = ingredientItem;
    }

    public ContainerSlot(IngredientItem ingredientItem, IngredientProcessModule processModule, ProcessSO processSO)
    {
        IngredientItem = ingredientItem;
        ProcessModule = processModule;
        ProcessSO = processSO;
    }


    public void Reset()
    {
        IngredientItem = null;
        ProcessModule = null;
        ProcessSO = null;
    }
}