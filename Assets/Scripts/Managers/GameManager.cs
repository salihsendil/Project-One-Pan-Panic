using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameConfigurationSO gameConfig;
    public GameConfigurationSO GameConfig => gameConfig;
}
