using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCardView : MonoBehaviour
{
    [SerializeField] private Image featuredImage;
    [SerializeField] private TMP_Text levelIndexText;
    [SerializeField] private TMP_Text levelNameText;
    [SerializeField] private GameObject lockedOverlay;
    [SerializeField] private TMP_Text highscoreText;

    public void SetCard(LevelMeta meta)
    {
        featuredImage.sprite = meta.LevelFeaturedImage;
        levelIndexText.text = meta.LevelIndex;
        levelNameText.text = meta.LevelName;
        lockedOverlay.SetActive(meta.IsLocked);
        highscoreText.text = "Highscore \n " + meta.Highscore;
    }
}
