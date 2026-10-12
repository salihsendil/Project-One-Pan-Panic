using UnityEngine;
using Zenject;

public class DeliveryModule : MonoBehaviour
{
    //References
    [Inject] private OrderManager orderManager;
    [Inject] private AdvancedPoolManager poolManager;

    [SerializeField] private RecipeMatcher recipeMatcher;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private FloatingTextController floatingText;

    private void Awake()
    {
        floatingText = GetComponentInChildren<FloatingTextController>();
    }

    public bool TryInteractionInstant(IInteractor interactor)
    {
        if (!interactor.HasItem) return false;

        if (!interactor.GetItem.Transform.TryGetComponent(out Plate plate))
        {
            floatingText.Show("Tabak Gerek!");
            return false;
        }

        if (recipeMatcher.TryGetRecipeMatch(plate.GetItems, out RecipeSO recipe))
        {
            if (orderManager.TryCompleteOrder(recipe))
            {
                interactor.RemoveItem();
                poolManager.Despawn(plate);
                particle.Play(true);
                return true;
            }
        }

        floatingText.Show("Yanlis Siparis!");
        return false;
    }
}
