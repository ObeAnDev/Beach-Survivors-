using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelUI : MonoBehaviour
{
    public List<UpgradeSO> upgrades;

    public Transform contentPanel;
    public UpgradeCardUI cardPrefab;

    public void GenerateCards()
    {
        ClearCards();

        List<UpgradeSO> selected = GetRandomUpgrades(3);

        foreach (var upgrade in selected)
        {
            UpgradeCardUI card =
                Instantiate(cardPrefab, contentPanel);

            card.Setup(upgrade);
        }
    }

    void ClearCards()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }
    }

    List<UpgradeSO> GetRandomUpgrades(int count)
    {
        List<UpgradeSO> pool = new List<UpgradeSO>(upgrades);
        List<UpgradeSO> result = new List<UpgradeSO>();

        for (int i = 0; i < count; i++)
        {
            if (pool.Count == 0)
                break;

            int index = Random.Range(0, pool.Count);

            result.Add(pool[index]);

            pool.RemoveAt(index);
        }

        return result;
    }
}
