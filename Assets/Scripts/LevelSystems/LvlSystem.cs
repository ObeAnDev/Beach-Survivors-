using System;
using UnityEngine;

[Serializable]
public class LvlSystem
{
    [SerializeField] float expCrrAmount;
    [SerializeField] float expNeededAmount;
    [SerializeField] int levelCrr;
    [SerializeField] int coinAmount;

    public float ExpCrrAmount => expCrrAmount;
    public float ExpNeededAmount => expNeededAmount;
    public int LevelCrr => levelCrr;
    public int CoinAmount => coinAmount;

    public LvlSystem(float exp, float needed, int level, int coins)
    {
        expCrrAmount = exp;
        expNeededAmount = needed;
        levelCrr = level;
        coinAmount = coins;
    }

    public void AddExp(float exp)
    {
        expCrrAmount += exp;

        while (expCrrAmount >= expNeededAmount)
        {
            LvlUp();
        }
    }

    void LvlUp()
    {
        expCrrAmount -= expNeededAmount;
        levelCrr++;
        expNeededAmount += 20;
    }

    public void AddCoin(int coin)
    {
        coinAmount += coin;
    }
}
