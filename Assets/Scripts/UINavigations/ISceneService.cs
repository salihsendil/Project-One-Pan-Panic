using UnityEngine;

public interface ISceneService
{
    public void LoadScene(string sceneName);

    public void LoadPreviousScene();

    public void QuitGame();
}
