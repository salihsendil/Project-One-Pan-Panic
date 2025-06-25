using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Recipe", menuName ="Scriptable Object/New Recipe")]
public class RecipeSO : ScriptableObject
{
    public string recipeName;
    public int successfulOrderPoint;
    public int failedOrderPenaltyPoint;
    public float preparationTime;
    [SerializeField] public List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();

    [System.Serializable]
    public struct RecipeIngredient
    {
        public KitchenItemSO kitchenItemSO;
        public KitchenItemState kitchenItemState;

        public RecipeIngredient(KitchenItemSO kitchenItemSO, KitchenItemState kitchenItemState)
        {
            this.kitchenItemSO = kitchenItemSO;
            this.kitchenItemState = kitchenItemState;
        }
    }
}
