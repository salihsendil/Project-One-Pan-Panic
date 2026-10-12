using UnityEngine;


public class TutorialCounterTarget : MonoBehaviour
{
    private IInteractableCounter interactableCounter;

    [Header("Target Type")]
    [SerializeField] private TutorialTargetType tutorialTargetType;

    [Header("Target Type")]
    [SerializeField] private GuidancePointer pointer;

    public TutorialTargetType TargetType { get => tutorialTargetType; }

    private void Awake()
    {
        interactableCounter = GetComponent<IInteractableCounter>();
        if (interactableCounter == null)
            Debug.LogError("Interactable Counter could not found!");

        pointer = GetComponentInChildren<GuidancePointer>();
    }

    public void SetInteractionsEnabled(bool isEnable)
    {
        interactableCounter.IsInteractionEnabled = isEnable;
    }

    public void SetPointer(bool isVisible, Sprite sprite = null)
    {
        if (isVisible)
            pointer.Show(sprite);
        else
            pointer.Hide();
    }
}
