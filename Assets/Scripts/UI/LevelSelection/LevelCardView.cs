using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(LoadSceneAction))]
public class LevelCardView : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button button;
    [SerializeField] private LoadSceneAction buttonAction;

    [Header("Level")]
    [SerializeField] private TMP_Text levelIndex;
    [SerializeField] private TMP_Text levelName;
    [SerializeField] private TMP_Text highscore;

    [Header("Level Lock")]
    [SerializeField] private GameObject lockOverlay;

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonAction = GetComponent<LoadSceneAction>();
    }

    public void Set(LevelMeta meta)
    {
        button.enabled = !meta.IsLocked;
        buttonAction.SceneToLoad = meta.Scene;

        levelIndex.text = meta.LevelIndex;
        levelName.text = meta.LevelName;

        highscore.text = meta.Highscore.ToString();

        lockOverlay.SetActive(meta.IsLocked);
    }
}
