using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelUI : MonoBehaviour
{
    public List<UpgradeSO> Upgrades;

    public Transform contentPanel;

    public void OnEnable()
    {
        foreach (var card in contentPanel.transform.GetComponentsInChildren<UpgradeCardUI>())
        {

        }
    }
}
