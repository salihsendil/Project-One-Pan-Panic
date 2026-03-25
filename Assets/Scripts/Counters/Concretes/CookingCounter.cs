using UnityEngine;

[RequireComponent(typeof(ItemInteractionModule))]
[RequireComponent(typeof(CookingModule))]
public class CookingCounter : BaseCounter
{
    private ItemSocket itemSocket;
    private CookingModule cookingModule;

    protected override void Awake()
    {
        itemSocket = GetComponent<ItemSocket>();
        cookingModule = GetComponent<CookingModule>();

        base.Awake();
    }

    public override void InteractionStarted(IInteractor interactor)
    {

    }
}