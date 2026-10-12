using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class DialoguePanelDisplay : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TutorialStepManager tutorialStepManager;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text diagolueText;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    public void Show(string msg)
    {
        Show();
        diagolueText.text = msg;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tutorialStepManager.OnDialogueWindowSkipTap();
    }
}
