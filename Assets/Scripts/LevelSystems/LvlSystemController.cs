using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlSystemController : MonoBehaviour
{

    public LvlSystem levelSystem;

    void Start()
    {
        levelSystem = new LvlSystem(0, 10, 1);
    }

    void Update()
    {
        PlayerEventBus.OnLevelChanged.Invoke(levelSystem.LevelCrr);

        if (Input.GetKeyDown(KeyCode.X))
        {
            levelSystem.AddExp(1);

            PlayerEventBus.OnExpChanged.Invoke(levelSystem.ExpCrrAmount / levelSystem.ExpNeededAmount);          
        }
    }
}
