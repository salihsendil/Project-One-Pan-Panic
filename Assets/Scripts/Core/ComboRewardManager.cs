
public class ComboRewardManager
{
    private float scoreMultiplier;
    private float currencyMultiplier;
    private float multiplierStep;

    public ComboRewardManager(LevelConfigSO config)
    {
        multiplierStep = config.ScoreMultiplierStepAmount;
        currencyMultiplier = config.ScoreCurrencyMultiplier;
    }

    public int GetRewardedScore(int score)
    {
        return (int)(score * scoreMultiplier);
    }

    public void UpdateComboState()
    {
        scoreMultiplier += multiplierStep;
    }

    public void ResetScoreMultiplier()
    {
        scoreMultiplier = 0f;
    }

    public int GetEarnedCurrency(int score)
    {
        return (int)(score * currencyMultiplier);
    }
}
