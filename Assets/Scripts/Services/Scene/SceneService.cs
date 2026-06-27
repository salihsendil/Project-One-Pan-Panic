using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneService : MonoBehaviour
{
    [Inject] private LoadingPanelHandler loadingPanel;
    [Inject] private SignalBus signalBus;

    [SerializeField] private ScenesEnum currentScene;

    public ScenesEnum CurrentScene => currentScene;

    private void Awake()
    {
        string activeScene = SceneManager.GetActiveScene().name;

        if (!Enum.TryParse(activeScene, false, out currentScene))
        {
            LoadSceneAsync(ScenesEnum.MainMenuScene);
        }
    }

    private IEnumerator LoadSceneAsync(ScenesEnum sceneToLoad)
    {
        bool transitionCompleted = false;
        loadingPanel.SetCanvasVisibility(true, () => transitionCompleted = true);
        yield return new WaitUntil(() => transitionCompleted == true);

        Scene activeScene = SceneManager.GetActiveScene();
        yield return new WaitForSeconds(0.4f);

        Camera oldSceneCam = Camera.main;
        if (oldSceneCam != null)
        {
            oldSceneCam.gameObject.SetActive(false);
        }

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneToLoad.ToString(), LoadSceneMode.Additive);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            if (op.progress >= 0.9f)
            {
                op.allowSceneActivation = true;
            }

            yield return null;
        }

        //float currentProgress = 0f;
        //while (op.progress < 0.9f || currentProgress < 1f)
        //{
        //    float progress = Mathf.Clamp01(op.progress / 0.9f);
        //    currentProgress = Mathf.MoveTowards(currentProgress, progress, Time.deltaTime * 0.5f);
        //    loadingPanel.UpdateLoadingBar(currentProgress);

        //    yield return null;
        //}

        //op.allowSceneActivation = true;
        //while (!op.isDone)
        //{
        //    yield return null;
        //}

        yield return new WaitForSeconds(0.8f);

        yield return SceneManager.UnloadSceneAsync(activeScene);

        currentScene = sceneToLoad;
        Scene newScene = SceneManager.GetSceneByName(currentScene.ToString());
        SceneManager.SetActiveScene(newScene);

        Camera newSceneCam = Camera.main;
        if (newSceneCam != null)
        {
            newSceneCam.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(0.3f);

        transitionCompleted = false;
        loadingPanel.SetCanvasVisibility(false, () => transitionCompleted = true);
        yield return new WaitUntil(() => transitionCompleted == true);

        yield return new WaitForSeconds(0.4f);

        signalBus.Fire(new SceneFullyLoadedSignal(currentScene));
    }

    public void StartLoadSceneAsync(ScenesEnum scene)
    {
        StartCoroutine(LoadSceneAsync(scene));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
