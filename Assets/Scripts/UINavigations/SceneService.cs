using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneService : MonoBehaviour, ISceneService
{
    private Stack<string> previousScenes = new();

    public void LoadScene(string sceneName)
    {
        SaveCurrentScene();
        SceneManager.LoadScene(sceneName);
    }
    public void LoadPreviousScene()
    {
        if (previousScenes != null)
        {
            if (previousScenes.Count > 0)
            {
                string sceneName = previousScenes.Pop();
                LoadScene(sceneName);
            }
        }
    }

    private void SaveCurrentScene()
    {
        if (previousScenes != null)
        {
            string sceneName = SceneManager.GetActiveScene().ToString();
            previousScenes.Push(sceneName);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
