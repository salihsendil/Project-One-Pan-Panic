using UnityEngine;

public class CookingModule : MonoBehaviour, IInstantModule
{

    private ProcessType[] processTypes = { ProcessType.Cook };
    private float processSpeed = 1f;

    public bool TryInteractionInstant(IInteractor interactor)
    {
        throw new System.NotImplementedException();
    }
}
