using UnityEngine;

public class Test_ProgressTracker : MonoBehaviour
{
    [SerializeField] private ProgressTracker progressTracker = new();

    [SerializeField] private float speed;
    [SerializeField] private float targetValue;
    [SerializeField] private float currentValue;
    [SerializeField] private float progress_Ratio;

    private void Start()
    {
        progressTracker.SetTarget(20f);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    progressTracker.Tick(Time.deltaTime * speed);
        //    targetValue = progressTracker.TargetValue;
        //    currentValue = progressTracker.CurrentValue;
        //    progress_Ratio = progressTracker.ProgressRatio;
        //}
    }
}
