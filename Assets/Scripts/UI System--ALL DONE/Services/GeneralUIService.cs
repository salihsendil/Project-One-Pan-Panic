using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GeneralUIService : MonoBehaviour
{
    [Inject] private DiContainer diContainer;
    [SerializeField] private UIConfigSO uiConfig;

    private GameObject currentUI;
    private SceneManagerBase currentSceneManager;
    [SerializeField] private List<UIActionButton> buttons = new();
    private Dictionary<UIButtonActionType, Action<UIActionButtonSO>> buttonActionMap;

    private void Awake()
    {

        buttonActionMap = new()
        {
            { UIButtonActionType.LoadScene, buttonData => LoadScene(buttonData.scene) },
            { UIButtonActionType.TogglePausePanel, buttonData => TogglePausePanel() },
            { UIButtonActionType.ToggleOptionsPanel, buttonData => ToggleOptionsPanel() },
            { UIButtonActionType.ToggleMusic, buttonData => ToggleMusic(buttonData) },
            { UIButtonActionType.ToggleSound, buttonData => ToggleSound(buttonData) },
            { UIButtonActionType.QuitGame, buttonData => QuitGame() },
        };
    }

    private void OnEnable()
    {
#if UNITY_EDITOR
        SceneEventService.OnSceneChanged -= OnSceneLoaded;
#endif

        SceneEventService.OnSceneChanged += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneEventService.OnSceneChanged -= OnSceneLoaded;
    }

    private void HandleButtonAction(UIActionButtonSO buttonData)
    {
        if (buttonActionMap.TryGetValue(buttonData.actionType, out var action))
        {
            action?.Invoke(buttonData);
        }

        else
        {
            Debug.LogError("There is no button action match!");
        }
    }


    #region LoadScene
    public void LoadScene(ScenesEnum scene)
    {
        UnRegisterActionButtons();
        SceneManager.LoadScene(scene.ToString());
    }

    private void OnSceneLoaded(ScenesEnum scene)
    {
        GameObject sceneUIPrefab = uiConfig.GetSceneUIPrefab(scene);

        if (currentUI != null)
        {
            Destroy(currentUI);
        }

        currentUI = diContainer.InstantiatePrefab(sceneUIPrefab, transform.parent);

        if (!currentUI.TryGetComponent(out currentSceneManager))
        {
            Debug.LogWarning($"ISceneManager Not Found");
        }

        RegisterActionButtons();
    }
    #endregion

    private void TogglePausePanel()
    {
        currentSceneManager.TogglePausePanel();
    }

    private void ToggleOptionsPanel()
    {
        currentSceneManager.ToggleOptionsPanel();
    }

    private void ToggleSound(UIActionButtonSO buttonData)
    {
        currentSceneManager.ToggleSound(buttonData);
    }

    private void ToggleMusic(UIActionButtonSO buttonData)
    {
        currentSceneManager.ToggleMusic(buttonData);
    }

    #region Helper Methods
    private void RegisterActionButtons()
    {
        var tempButtons = currentUI.GetComponentsInChildren<UIActionButton>();

        foreach (var button in tempButtons)
        {
            button.OnActionButtonClicked += HandleButtonAction;
            buttons.Add(button);
        }
    }

    private void UnRegisterActionButtons()
    {
        if (buttons.Count > 0)
        {
            foreach (var button in buttons)
            {
                button.OnActionButtonClicked -= HandleButtonAction;
            }
        }
        buttons.Clear();
    }
    #endregion

    private void QuitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}
