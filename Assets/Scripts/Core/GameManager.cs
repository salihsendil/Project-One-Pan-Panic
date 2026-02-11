using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    //Zenject
    [Inject] private SignalBus signalBus;

    //Config
    [SerializeField] private LevelConfigSO levelConfig;

    //Timer
    private Timer timer = new();
    private int currentTime;

    private void Awake()
    {
        currentTime = levelConfig.levelTime;
        timer.SetTimer(currentTime);
    }

    private void Update()
    {
        CheckAndUpdateTimer();
    }

    #region GameStart
    #endregion

    #region Timer

    private void CheckAndUpdateTimer()
    {
        timer.TickTimer();
        if ((int)timer.Remaining != currentTime)
        {
            currentTime = (int)timer.Remaining;
            signalBus.Fire(new LevelTimerTickSignal(currentTime));
        }
        if (timer.IsFinished())
        {
            Debug.Log("game finished!");
        }
    }

    #endregion

}
