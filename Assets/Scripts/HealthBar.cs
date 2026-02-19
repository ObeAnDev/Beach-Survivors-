using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    void Awake()
    {
        PlayerEventBus.OnHealthChanged += HealthUpdate;
    }

    void Update()
    {
        
    }
    public void HealthUpdate(float HealthPercent)
    {
        healthBar.fillAmount = HealthPercent;
    }
}
