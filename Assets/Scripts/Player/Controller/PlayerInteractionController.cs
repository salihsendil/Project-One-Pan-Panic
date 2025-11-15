using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    private PlayerInteractor playerInteractor;
    private PlayerCarryingController playerCarryingController;

    private void Awake()
    {
        TryGetComponent(out playerInteractor);
        TryGetComponent(out playerCarryingController);

        if (playerInteractor == null) { Debug.LogWarning("Please add PlayerInteractor.cs component"); }
        if (playerCarryingController == null) { Debug.LogWarning("Please add PlayerCarryingController.cs component"); }

    }

    private void OnEnable()
    {
        playerInteractor.OnCounterInteractionRequest += InteractionRequestRouter;
    }

    private void OnDisable()
    {
        playerInteractor.OnCounterInteractionRequest -= InteractionRequestRouter;
    }

    private void InteractionRequestRouter(IInteractable<PlayerCarryingController> interactable)
    {
        interactable.Interact(playerCarryingController);
    }

}
