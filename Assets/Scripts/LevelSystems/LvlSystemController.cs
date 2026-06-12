using UnityEngine;

public class LvlSystemController : MonoBehaviour
{
    public LvlSystem levelSystem;

    void Start()
    {
        levelSystem = new LvlSystem(0, 10, 1, 0);
    }

    public void AddExp(float expAmount)
    {
        int oldLevel = levelSystem.LevelCrr;

        levelSystem.AddExp(expAmount);

        PlayerEventBus.OnExpChanged?.Invoke(
            levelSystem.ExpCrrAmount / levelSystem.ExpNeededAmount
        );

        if (levelSystem.LevelCrr != oldLevel)
        {
            PlayerEventBus.OnLevelChanged?.Invoke(levelSystem.LevelCrr);
            PlayerEventBus.OnLevelUp?.Invoke();
        }
    }

    public void AddCoin(int coinAmount)
    {
        levelSystem.AddCoin(coinAmount);
        PlayerEventBus.OnCoinChange?.Invoke(levelSystem.CoinAmount);
    }
}
