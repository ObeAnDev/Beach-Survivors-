using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] Image expBar;
    [SerializeField] TextMeshProUGUI lvlText;

    void Awake()
    {
        PlayerEventBus.OnExpChanged += expUpdate;
        PlayerEventBus.OnLevelChanged += lvlUpdate;
    }
    public void expUpdate(float ExpPercent)
    {
        expBar.fillAmount = ExpPercent;
    }
    public void lvlUpdate(int lvl)
    {
        lvlText.text = lvl + "LvL";
    }
}
