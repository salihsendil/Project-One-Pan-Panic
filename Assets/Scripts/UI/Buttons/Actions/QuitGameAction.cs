using UnityEngine;
using Zenject;

[RequireComponent(typeof(UIButtonHandler))]
public class QuitGameAction : BaseUIButtonAction
{
    [Inject] private SceneService sceneService;

    public override void Execute()
    {
        sceneService.QuitGame();
    }
}
