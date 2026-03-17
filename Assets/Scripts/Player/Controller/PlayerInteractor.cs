using System;
using UnityEngine;
using Zenject;

public class PlayerInteractor : MonoBehaviour
{
    //Zenject
    [Inject] private InputHandler inputHandler;
    [Inject] private SignalBus signalBus;

    //Ray Variables
    [SerializeField] private float rayRadius = 0.25f;
    [SerializeField] private float maxRayDistance = 1f;
    [SerializeField] private Vector3 rayOffset = new(0f, 0.75f, 0f);

    private BaseCounter currentCounter;
    private CounterHighlighter currentHighlighter;

    //Events
    public event Action<IInteractable<PlayerCarryingController>> OnCounterInteractionRequest;
    public event Action<IInteractableAlternate> OnCounterInteractionAlternateRequest;

    private void OnEnable()
    {
        inputHandler.OnInteractionButtonPressed += HandleInteraction;
        inputHandler.OnInteractionAlternateButtonPressed += HandleInteractionAlternate;
    }

    private void OnDisable()
    {
        inputHandler.OnInteractionButtonPressed -= HandleInteraction;
        inputHandler.OnInteractionAlternateButtonPressed -= HandleInteractionAlternate;

    }

    void Update()
    {
        UpdateHighlight();

    }

    private void UpdateHighlight()
    {
        if (TryGetInteractable(out GameObject go))
        {
            if (go.TryGetComponent(out CounterHighlighter newHighlighter))
            {
                if (currentHighlighter == newHighlighter) { return; }

                if (currentHighlighter != null) { currentHighlighter.HighlightObject(false); }

                currentHighlighter = newHighlighter;
                currentHighlighter.HighlightObject(true);
                return;
            }
        }

        else if (currentHighlighter != null)
        {
            currentHighlighter.HighlightObject(false);
            currentHighlighter = null;
        }
    }

    private bool TryGetInteractable(out GameObject go)
    {
        go = null;

        Vector3 rayOrigin = transform.position + rayOffset;
        Vector3 rayDir = transform.forward * maxRayDistance;
        Ray ray = new(rayOrigin, rayDir);

        if (Physics.SphereCast(ray, rayRadius, out RaycastHit hit, maxRayDistance))
        {
            go = hit.collider.gameObject;
            return true;
        }
        return false;
    }

    private void HandleInteraction()
    {
        if (TryGetInteractable(out GameObject go))
        {
            if (go.TryGetComponent(out IInteractable<PlayerCarryingController> interactable))
            {
                OnCounterInteractionRequest?.Invoke(interactable);
            }
        }
    }

    private void HandleInteractionAlternate()
    {
        if (TryGetInteractable(out GameObject go))
        {
            if (go.TryGetComponent(out IInteractableAlternate interactableAlternate))
            {
                OnCounterInteractionAlternateRequest?.Invoke(interactableAlternate);
            }
        }
    }
}
