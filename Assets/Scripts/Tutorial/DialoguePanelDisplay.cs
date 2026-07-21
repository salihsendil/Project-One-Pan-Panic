using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;

public class DialoguePanelDisplay : MonoBehaviour, IPointerClickHandler
{
    [Inject] private SignalBus signalBus;
    [Inject] private TutorialStepManager tutorialStepManager;

    private RectTransform rectTransform;
    private Vector2 originalPosition;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text skipText;
    [SerializeField] private Image mascotImage;

    private void Awake()
    {
        skipText.DOFade(0.1f, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        rectTransform = GetComponent<RectTransform>();
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

        Pop();

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

    public void Pop()
    {
        rectTransform.DOKill();

        // 1. Objeyi ekranýn altýna kaydýr ve ölçeðini sýfýrla
        rectTransform.anchoredPosition = originalPosition - new Vector2(0, 500f);
        rectTransform.localScale = Vector3.zero;

        if (canvasGroup.alpha == 0) canvasGroup.alpha = 1;

        // 2. Ekranýn altýndan kendi yerine gelsin (OutBack ile hafif esþeyerek durur)
        rectTransform.DOAnchorPos(originalPosition, 0.5f).SetEase(Ease.OutBack);

        // 3. Ayný anda büyüsün ve yerine vardýðýnda punch etkisi versin
        rectTransform.DOScale(Vector3.one, 0.5f * 0.7f).OnComplete(() =>
        {
            rectTransform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.2f, 5, 0.5f);
        });


    }
}
