using UnityEngine;
using Zenject;

[RequireComponent(typeof(UIButtonHandler))]
public class LoadSceneAction : BaseUIAction
{
    [Inject] private SceneService sceneService;
    [SerializeField] private ScenesEnum sceneToLoad;

    public override void Execute()
    {
        sceneService.LoadScene(sceneToLoad);
    }
}