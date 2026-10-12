using UnityEngine;


// This state module only accepts Cookware Items.
// The restriction is applied by the Stove Counter.
// Its mission is simple: make the Cookware Item hot.

public class HeatingModule : MonoBehaviour, IStateModule
{
    private IInteractor currentInteractor;

    public void Initialize(IInteractor interactor)
    {
        currentInteractor = interactor;
        currentInteractor.OnItemPlaced += ItemPlaced;
        currentInteractor.OnItemRemoved += ItemRemoved;
        Debug.Log("it is came as param: " + currentInteractor.ToString() + "cooking module initialize");
    }

    public void Dispose()
    {
        currentInteractor.OnItemPlaced -= ItemPlaced;
        currentInteractor.OnItemRemoved -= ItemRemoved;
        Debug.Log("it is gonna go as param: " + currentInteractor.ToString() + "cooking module dispose");
        currentInteractor = null;
    }

    private void ItemPlaced(IPickable pickable)
    {
        CookwareItem item = (CookwareItem)pickable;
        item.IsHot = true;
    }

    private void ItemRemoved(IPickable pickable)
    {
        CookwareItem item = (CookwareItem)pickable;
        item.IsHot = false;
    }
}
