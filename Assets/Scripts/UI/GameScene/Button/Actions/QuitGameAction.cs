using UnityEngine;
using Zenject;

[RequireComponent(typeof(UIButtonHandler))]
public class QuitGameAction : BaseUIAction
{
    [Inject] private SceneService sceneService;

    public override void Execute()
    {
        sceneService.QuitGame();
    }
}
