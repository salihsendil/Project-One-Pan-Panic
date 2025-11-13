using UnityEngine;

public interface IItemHolder /*: IInteractableModule - basecounterda itemholdermodule'u zorunlu tutmamak için kullanabiliriz*/
{
    public bool HasItem();
    public GameObject GetItem();
    public void SetItem(GameObject obj);
    public GameObject RemoveItem();
}
