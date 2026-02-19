using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneService : MonoBehaviour
{
    public void LoadScene(ScenesEnum sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad.ToString());
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
