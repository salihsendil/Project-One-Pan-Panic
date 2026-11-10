using UnityEngine;
using Zenject;

public class DeliveryModule : MonoBehaviour, IInstantInteractionModule
{
    //References
    [Inject] private OrderManager orderManager;
    [Inject] private AdvancedPoolManager poolManager;
    [Inject] private RecipeMatcher recipeMatcher;

    [SerializeField] private ParticleSystem particle;
    [SerializeField] private FloatingTextController floatingText;

    private void Awake()
    {
        floatingText = GetComponentInChildren<FloatingTextController>();
    }

    public bool TryInteractInstant(IInteractor incomingInteractor)
    {
        if (!incomingInteractor.HasItem) return false;

        if (!incomingInteractor.GetItem.Transform.TryGetComponent(out Plate plate))
        {
            floatingText.Show("!!!!");
            Debug.Log("tabak yok");
            return false;
        }

        if (recipeMatcher.TryGetRecipeMatch(plate.GetItems, out RecipeSO recipe))
        {
            Debug.Log("buraya hiç girmedi");
            if (orderManager.TryCompleteOrder(recipe))
            {
                incomingInteractor.RemoveItem();
                poolManager.Despawn(plate);
                particle?.Play(true);
                return true;
            }
        }

        floatingText.Show("!!!!");
        Debug.Log("tarif eþleþmedi");
        return false;
    }
}
