using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class UIButtonHandler : MonoBehaviour
{
    [SerializeField] private Button button;
    private BaseUIAction[] uiActions;

    [Inject] private AudioService audioService;

    private void Awake()
    {
        if (button == null) { TryGetComponent(out button); }

        uiActions = GetComponents<BaseUIAction>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(ExecuteActions);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(ExecuteActions);
    }

    private void ExecuteActions()
    {
        audioService.PlaySFX(SFXType.UI_Button_Click);

        foreach (var action in uiActions)
        {
            if (action == null) { continue; }

            action.Execute();
        }
    }
}