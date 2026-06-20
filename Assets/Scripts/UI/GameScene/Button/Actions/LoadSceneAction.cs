using UnityEngine;
using Zenject;

[RequireComponent(typeof(UIButtonHandler))]
public class LoadSceneAction : BaseUIAction
{
    [Inject] private SceneService sceneService;
    [SerializeField] private ScenesEnum sceneToLoad;

    public override void Execute()
    {
        Time.timeScale = 1;
        sceneService.LoadScene(sceneToLoad);
    }
}