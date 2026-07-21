using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelCardView : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image featuredImage;
    [SerializeField] private TMP_Text levelIndexText;
    [SerializeField] private TMP_Text levelNameText;
    [SerializeField] private GameObject lockedOverlay;
    [SerializeField] private TMP_Text highscoreText;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void SetCard(LevelMeta meta)
    {
        button.enabled = !meta.IsLocked;
        featuredImage.sprite = meta.LevelFeaturedImage;
        levelIndexText.text = meta.LevelIndex;
        levelNameText.text = meta.LevelName;
        lockedOverlay.SetActive(meta.IsLocked);
        highscoreText.text = "Highscore \n " + meta.Highscore;
    }
}
