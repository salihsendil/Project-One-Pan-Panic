using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonHandler : MonoBehaviour
{
    [SerializeField] private Button button;
    private BaseUIAction[] uiActions;

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
        foreach (var action in uiActions)
        {
            if (action == null) { continue; }

            action.Execute();
        }
    }
}