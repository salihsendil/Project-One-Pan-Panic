using UnityEngine;

public class CookingModule : MonoBehaviour, IInstantModule
{
    private ProcessType processType = ProcessType.Cut;
    private float processSpeed = 1f;

    private ProcessType[] processTypes = { ProcessType.Cook, ProcessType.Burn };

    public bool TryInteractionInstant(IInteractor interactor)
    {
        throw new System.NotImplementedException();
    }
}
