using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneService : MonoBehaviour
{
    [Inject] private LoadingPanelHandler loadingPanel;
    [Inject] private SignalBus signalBus;

    public void LoadScene(ScenesEnum sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad.ToString());
    }

    public void StartLoadSceneAsync(ScenesEnum scene)
    {
        StartCoroutine(LoadSceneAsync(scene));
    }

    private IEnumerator LoadSceneAsync(ScenesEnum sceneToLoad)
    {
        bool transitionCompleted = false;
        loadingPanel.SetCanvasVisibility(true, () => transitionCompleted = true);
        yield return new WaitUntil(() => transitionCompleted == true);

        Scene currentScene = SceneManager.GetActiveScene();
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

        yield return SceneManager.UnloadSceneAsync(currentScene);

        Scene newScene = SceneManager.GetSceneByName(sceneToLoad.ToString());
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

        signalBus.Fire(new SceneFullyLoadedSignal());
    }

    public void QuitGame()
    {
        Debug.Log("quitting...");

        Application.Quit();
    }
}
