using UnityEngine;


// All counters inherit from this class.
// Counters look for their modules and cache them in an array at Awake.
// When an interaction type is received, this class tries to call the corresponding method.
//
// The array size is fixed but easily extendable.
//
// If a new counter is needed, just add the counter type (e.g. DrinkCounter) and create its module
// (e.g. DrinkModule). DrinkCounter must inherit from this class.
// If needed, it can override the methods.
//
// All classes that inherit from this class only work as routers.
// They call the related module depending on the interaction type.
// Module classes are the actual performers.


[RequireComponent(typeof(CounterHighlighter))]
public abstract class BaseCounter : MonoBehaviour, IInteractableCounter
{
    //References
    [SerializeField] protected bool isInteractionEnabled = true;
    protected CounterHighlighter counterHighlighter;

    //Modules
    protected IInstantInteractionModule[] instantInteractionModules = new IInstantInteractionModule[3];
    protected IHoldInteractionModule[] holdInteractionModules = new IHoldInteractionModule[2];
    protected IStateModule[] stateModules = new IStateModule[2];

    public bool IsInteractionEnabled
    {
        get => isInteractionEnabled;
        set
        {
            HighlightInteractable(false);
            isInteractionEnabled = value;
        }
    }


    protected virtual void Awake()
    {
        counterHighlighter = GetComponent<CounterHighlighter>();

        instantInteractionModules = GetComponents<IInstantInteractionModule>();
        holdInteractionModules = GetComponents<IHoldInteractionModule>();
        stateModules = GetComponents<IStateModule>();
    }

    public virtual void InteractInstant(IInteractor incomingInteractor)
    {
        if (!isInteractionEnabled)
            return;

        foreach (var module in instantInteractionModules)
        {
            module?.TryInteractInstant(incomingInteractor);
        }
    }

    public virtual void InteractHoldStarted(IInteractor incomingInteractor)
    {
        if (!isInteractionEnabled)
            return;

        foreach (var module in holdInteractionModules)
        {
            module?.OnHoldInteractionStarted();
        }
    }

    public virtual void InteractHoldCanceled(IInteractor incomingInteractor)
    {
        if (!isInteractionEnabled)
            return;

        foreach (var module in holdInteractionModules)
        {
            module?.OnHoldInteractionCanceled();
        }
    }

    public void HighlightInteractable(bool canInteractable)
    {
        if (isInteractionEnabled)
        counterHighlighter.HighlightObject(canInteractable);
    }
}
