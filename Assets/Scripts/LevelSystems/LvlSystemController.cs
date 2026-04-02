using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlSystemController : MonoBehaviour
{

    public LvlSystem levelSystem;

    void Start()
    {
        levelSystem = new LvlSystem(0, 10, 1, 0);
    }

    void Update()
    {

    }

    public void AddExp(float expAmount)
    {
        PlayerEventBus.OnLevelChanged.Invoke(levelSystem.LevelCrr);

        levelSystem.AddExp(expAmount);

        PlayerEventBus.OnExpChanged.Invoke(levelSystem.ExpCrrAmount / levelSystem.ExpNeededAmount);        
    }
    public void addCoin(int coinAmount)
    {
        levelSystem.AddCoin(coinAmount);

        PlayerEventBus.OnCoinChange.Invoke(levelSystem.CoinAmount);
    }
}
