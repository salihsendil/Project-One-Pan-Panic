using UnityEngine;

public class TutorialTarget : MonoBehaviour
{
    [SerializeField] private TutorialWorldTarget tutorialWorldTarget;

    public TutorialWorldTarget TutorialWorldTarget => tutorialWorldTarget;

}
