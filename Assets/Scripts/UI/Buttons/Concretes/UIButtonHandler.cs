using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class UIButtonHandler : MonoBehaviour
{
    [SerializeField] private Button button;
    private BaseUIButtonAction[] buttonActions;

    //[Inject] private SFXService sfxService;

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonActions = GetComponents<BaseUIButtonAction>();
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
        //sfxService.PlaySFXOneShot(SFXType.UIButtonClick);

        foreach (var action in buttonActions)
        {
            if (action == null) { continue; }

            action.Execute();
        }
    }
}