using System;
using UnityEngine;

public class TutorialTarget : MonoBehaviour
{
    public static event Action<TutorialTarget> OnSpawned;
    public static event Action<TutorialTarget> OnDespawned;

    [SerializeField] private TutorialWorldTarget tutorialWorldTarget;

    public TutorialWorldTarget TutorialWorldTarget => tutorialWorldTarget;

    private void Start()
    {
        OnSpawned?.Invoke(this);
    }

    private void OnDisable()
    {
        OnDespawned?.Invoke(this);
    }

}
