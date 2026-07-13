using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillManager : MonoBehaviour
{
    public static KillManager instance;

    private int killCount;
    public int KillCount => killCount;

    private void Awake()
    {
        instance = this;
    }

    public void AddKill()
    {
        killCount++;
        Debug.Log("Kills: " + killCount);
    }
}
