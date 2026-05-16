using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpgradeSO : UpgradeSO
{
    public float upgSpeed;

    public void AddSpeed(Transform player)
    {
        player.GetComponent<PlayerControllerManager>().Speed += upgSpeed;
    }
}
