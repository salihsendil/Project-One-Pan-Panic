using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelSelectionPanelDisplay : MonoBehaviour
{
    [SerializeField] private LevelCatalogSO levelCatalog;

    [SerializeField] private LevelCardView cardPrefab;
    [SerializeField] private Transform cardsParentTransform;

    //zenject gelince bunu construct içine alalým onenable içinde animasyon yapalým

    private void Start()
    {
        List<LevelMeta> levels = levelCatalog.Levels;

        foreach (var level in levels)
        {
            LevelCardView card = Instantiate(cardPrefab, cardsParentTransform);
            card.Set(level);
        }

        //gameObject.SetActive(false);
    }
}
