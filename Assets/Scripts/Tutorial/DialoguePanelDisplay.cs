using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;

public class DialoguePanelDisplay : MonoBehaviour, IPointerClickHandler
{
    [Inject] private SignalBus signalBus;

    [SerializeField] private TutorialStepManager tutorialStepManager;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text skipText;
    [SerializeField] private Image mascotImage;

    private void Awake()
    {
        skipText.DOFade(0.1f, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnEnable()
    {
        signalBus.Subscribe<NewTutorialStepSignal>(SetPanel);
        signalBus.Subscribe<GameStartedSignal>(DisablePanel);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<NewTutorialStepSignal>(SetPanel);
        signalBus.Unsubscribe<GameStartedSignal>(DisablePanel);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tutorialStepManager.OnScreenTap();
    }

    private void SetPanel(NewTutorialStepSignal signal)
    {
        dialogueText.text = signal.TutorialStep.Dialogue;
        mascotImage.sprite = signal.TutorialStep.Mascot;

        bool isSkippable = signal.TutorialStep.CompletionCondition == TutorialCompletionCondition.Tap ? true : false;
        SetSkipText(isSkippable);
    }

    private void SetSkipText(bool isOpen)
    {
        skipText.enabled = canvasGroup.interactable = canvasGroup.blocksRaycasts = isOpen;
    }

    private void DisablePanel()
    {
        gameObject.SetActive(false);
    }
}
