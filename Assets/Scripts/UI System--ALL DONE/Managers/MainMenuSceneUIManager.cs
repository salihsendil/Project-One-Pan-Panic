using UnityEngine;
using UnityEngine.UI;

public class MainMenuSceneUIManager : SceneManagerBase
{
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private Image musicButton;
    [SerializeField] private Image soundButton;

    void Start()
    {
        optionsPanel.SetActive(false);
    }

    public override void ToggleOptionsPanel()
    {
        optionsPanel.SetActive(!optionsPanel.activeInHierarchy);
    }

    public override void ToggleMusic(UIActionButtonSO buttonData)
    {
        musicButton.sprite = (musicButton.sprite == buttonData.toggleOnSprite) ? buttonData.toggleOffSprite : buttonData.toggleOnSprite;
    }

    public override void ToggleSound(UIActionButtonSO buttonData)
    {
        soundButton.sprite = (soundButton.sprite == buttonData.toggleOnSprite) ? buttonData.toggleOffSprite: buttonData.toggleOnSprite;
    }
}
