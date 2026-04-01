using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LvlSystem
{
    [SerializeField] float expCrrAmount;
    [SerializeField] float expNeededAmount;
    [SerializeField] int levelCrr;
    public float ExpCrrAmount => expCrrAmount;
    public float ExpNeededAmount => expNeededAmount;
    public int LevelCrr => levelCrr;


    public LvlSystem(float _expCrrAmount, float _expNeededAmount, int _levelCrr)
    {
        expCrrAmount = _expCrrAmount;
        expNeededAmount = _expNeededAmount;
        levelCrr = _levelCrr;
    }
    
    public void AddExp(float exp)
    {
        expCrrAmount += exp;
        Debug.Log(expCrrAmount);

        while (expCrrAmount >= expNeededAmount)
        {
            LvlUp();
        }

        Debug.Log(levelCrr);
    }

    public void LvlUp()
    {
        expCrrAmount -= expNeededAmount;
        levelCrr ++;

        expNeededAmount += 20;
    }
}
