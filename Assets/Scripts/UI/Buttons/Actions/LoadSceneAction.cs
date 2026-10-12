using UnityEngine;
using Zenject;

[RequireComponent(typeof(UIButtonHandler))]
public class LoadSceneAction : BaseUIButtonAction
{
    [Inject] private SceneService sceneService;
    [SerializeField] private ScenesEnum sceneToLoad;

    public ScenesEnum SceneToLoad { get => sceneToLoad; set => sceneToLoad = value; }

    public override void Execute()
    {
        sceneService.StartLoadSceneAsync(sceneToLoad);
    }
}