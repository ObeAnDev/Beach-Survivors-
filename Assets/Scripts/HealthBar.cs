using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void HealthUpdate(float HealthPercent)
    {
        healthBar.fillAmount = HealthPercent;
    }
}
