using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup mainMenuGroup;
    [SerializeField] private CanvasGroup customizeGroup;

    private void OnEnable()
    {
        GoToMainMenu();
    }

    public void GoToCustomize()
    {
        customizeGroup.alpha = 1;
        customizeGroup.interactable = true;
        mainMenuGroup.alpha = 0;
        mainMenuGroup.interactable = false;
    }

    public void GoToMainMenu()
    {
        customizeGroup.alpha = 0;
        customizeGroup.interactable = false;
        mainMenuGroup.alpha = 1;
        mainMenuGroup.interactable = true;
    }
}
