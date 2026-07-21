using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelSelectionPanelDisplay : MonoBehaviour
{
    [Inject] private LevelDataService levelDataService;

    [SerializeField] private List<LevelCardView> cards = new();

    private void Start()
    {
        Debug.Log("çalýþtý");
        List<LevelMeta> levels = levelDataService.LevelCatalog.Levels;

        int safeLoopCount = Mathf.Min(cards.Count, levels.Count);

        for (int i = 0; i < safeLoopCount; i++)
        {
            if (cards[i] == null || levels[i] == null) continue;
            cards[i].SetCard(levels[i]);
        }
    }
}
