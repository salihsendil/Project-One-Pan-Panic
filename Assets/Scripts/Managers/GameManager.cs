using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;

    public GameSettings Settings  => gameSettings;
}
