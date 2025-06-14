using UnityEngine;

public interface IKitchenItemStateProvider
{
    public KitchenItemState CurrentState { get; set; }
}
