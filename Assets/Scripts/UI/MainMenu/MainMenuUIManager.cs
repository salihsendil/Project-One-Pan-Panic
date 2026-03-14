using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup mainMenuGroup;
    [SerializeField] private CanvasGroup customizeGroup;

    private void OnEnable()
    {
        customizeGroup.alpha = 0;
        mainMenuGroup.alpha = 1;
    }

    public void GoToCustomize()
    {
        customizeGroup.alpha = 1;
        mainMenuGroup.alpha = 0;
    }

    public void GoToMainMenu()
    {
        customizeGroup.alpha = 0;
        mainMenuGroup.alpha = 1;
    }
}
