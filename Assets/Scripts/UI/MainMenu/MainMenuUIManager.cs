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
        customizationUI.SetVisibility(true);
        mainMenuCanvas.alpha = 0;
        mainMenuCanvas.interactable = false;
        mainMenuCanvas.blocksRaycasts = false;
    }

    public void GoToMainMenu()
    {
        customizationUI.SetVisibility(false);
        mainMenuCanvas.alpha = 1;
        mainMenuCanvas.interactable = true;
        mainMenuCanvas.blocksRaycasts = true;
    }
}
