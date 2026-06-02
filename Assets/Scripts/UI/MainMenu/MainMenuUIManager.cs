using DG.Tweening;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup mainMenuCanvas;
    [SerializeField] private CustomizationUIController customizationUI;

    private void OnEnable()
    {
        GoToMainMenu();
    }

    public void GoToCustomize()
    {
        mainMenuCanvas.transform.DOKill();

        mainMenuCanvas.interactable = false;
        mainMenuCanvas.blocksRaycasts = false;

        mainMenuCanvas.transform.DOScale(Vector3.zero, 0.2f)
            .SetEase(Ease.InQuad)
            .SetUpdate(true);

        customizationUI.SetVisibility(true);
    }

    public void GoToMainMenu()
    {
        mainMenuCanvas.transform.DOKill();

        mainMenuCanvas.transform.localScale = Vector3.zero;

        mainMenuCanvas.transform.DOScale(Vector3.one, 0.4f)
            .SetEase(Ease.OutQuad)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                mainMenuCanvas.interactable = true;
                mainMenuCanvas.blocksRaycasts = true;
            });

        customizationUI.SetVisibility(false);
    }
}
